using Moq;

using ScheduledMeets.Internals.Extensions;
using ScheduledMeets.TestTools.Extensions;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace ScheduledMeets.Internals.Tests
{
    public class FreshTests
    {
        private static readonly TimeSpan _timeout = TimeSpan.FromMilliseconds(50);
        private static readonly object[] NotCancelledCancellationTokensParams = new object[]
        {
            default(CancellationToken),
            new CancellationTokenSource().Token,
        };

        public interface IWorker<TResult>
        {
            Task<TResult> DoAsync();
        }

        public interface IReviewer
        {
            bool Inspect<TParameter>(TParameter parameter);
        }

        class Manager<T>
        {
            public T? Current { get; private set; }

            [DebuggerStepThrough]
            [return:NotNullIfNotNull("value")]
            public T? Next(T? value) => Current = value;
        }

        class TestException : Exception
        {
            public TestException() { }
            public TestException(string message) : base(message) { }
        }

        [Test]
        [TestCaseSource(nameof(NotCancelledCancellationTokensParams))]
        public async Task BasicFlow(CancellationToken cancellationToken)
        {
            const int value = 5;

            TaskCompletionSource<int> taskSource = new();
            Mock<IWorker<int>> worker = new();
            worker
                .Setup(_ => _.DoAsync())
                .Returns(() => taskSource.Task);
            Mock<IReviewer> reviewer = new();
            reviewer
                .Setup(_ => _.Inspect(It.IsAny<int>()))
                .Returns(() => true);

            Fresh<int> sut = new(worker.Object.DoAsync, reviewer.Object.Inspect);
            Task<int> valueTask = sut.GetAsync(cancellationToken);

            Assert.That(!valueTask.IsCompleted);
            taskSource.SetResult(value);
            int result = await valueTask.SetTimeout(_timeout);

            Assert.That(value, Is.EqualTo(result));
            worker.Verify(_ => _.DoAsync(), Times.Once);
            reviewer.Verify(_ => _.Inspect(It.IsAny<int>()), Times.Never);
        }

        [Test]
        [TestCaseSource(nameof(NotCancelledCancellationTokensParams))]
        public async Task TwoSimultaneousRequestsFlow(CancellationToken cancellationToken)
        {
            const int value = 5;

            TaskCompletionSource<int> taskSource = new();
            Mock<IWorker<int>> worker = new();
            worker
                .Setup(_ => _.DoAsync())
                .Returns(() => taskSource.Task);
            Mock<IReviewer> reviewer = new();
            reviewer
                .Setup(_ => _.Inspect(It.IsAny<int>()))
                .Returns(() => true);

            Fresh<int> sut = new(worker.Object.DoAsync, reviewer.Object.Inspect);
            Task<int> valueTaskA = sut.GetAsync(cancellationToken);
            Task<int> valueTaskB = sut.GetAsync(cancellationToken);

            taskSource.SetResult(value);

            (int result1, int result2) = await valueTaskA.And(valueTaskB).SetTimeout(_timeout);

            Assert.That(result1, Is.EqualTo(value));
            Assert.That(result2, Is.EqualTo(value));
            worker.Verify(_ => _.DoAsync(), Times.Once);
            reviewer.Verify(_ => _.Inspect(It.Is<int>(p => p == value)), Times.Once);
        }

        [Test]
        [TestCaseSource(nameof(NotCancelledCancellationTokensParams))]
        public async Task TwoGradualRequestsFlowNoRefreshNeeded(CancellationToken cancellationToken)
        {
            const int value = 5;

            TaskCompletionSource<int> taskSource = new();
            Mock<IWorker<int>> worker = new();
            worker
                .Setup(_ => _.DoAsync())
                .Returns(() => taskSource.Task);
            Mock<IReviewer> reviewer = new();
            reviewer
                .Setup(_ => _.Inspect(It.IsAny<int>()))
                .Returns(() => true);

            Fresh<int> sut = new(worker.Object.DoAsync, reviewer.Object.Inspect);

            Task<int> valueTaskA = sut.GetAsync(cancellationToken);

            taskSource.SetResult(value);
            int result1 = await valueTaskA.SetTimeout(_timeout);

            int result2 = await sut.GetAsync(cancellationToken).SetTimeout(_timeout);

            Assert.That(result1, Is.EqualTo(value));
            Assert.That(result2, Is.EqualTo(value));
            worker.Verify(_ => _.DoAsync(), Times.Once);
            reviewer.Verify(_ => _.Inspect(It.Is<int>(p => p == value)), Times.Once);
        }

        [Test]
        [TestCaseSource(nameof(NotCancelledCancellationTokensParams))]
        public async Task TwoGradualRequestsRefreshNeededFlow(CancellationToken cancellationToken)
        {
            Manager<TaskCompletionSource<int>> taskSourceManager = new();
            Manager<int> valueManager = new();
            Mock<IWorker<int>> worker = new();
            worker
                .Setup(_ => _.DoAsync())
                .Returns(() => taskSourceManager.Current!.Task);
            Mock<IReviewer> reviewer = new();
            reviewer
                .Setup(_ => _.Inspect(It.IsAny<int>()))
                .Returns<int>(value => valueManager.Current == value);

            Fresh<int> sut = new(worker.Object.DoAsync, reviewer.Object.Inspect);

            TaskCompletionSource<int> taskSource1 = taskSourceManager.Next(new());
            int value1 = valueManager.Next(5);
            Task<int> valueTaskA = sut.GetAsync(cancellationToken);

            taskSource1.SetResult(value1);
            int result1 = await valueTaskA.SetTimeout(_timeout);

            TaskCompletionSource<int> taskSource2 = taskSourceManager.Next(new());
            int value2 = valueManager.Next(6);
            Task<int> valueTaskB = sut.GetAsync(cancellationToken);

            taskSource2.SetResult(value2);
            int result2 = await valueTaskB.SetTimeout(_timeout);

            Assert.That(result1, Is.EqualTo(value1));
            Assert.That(result2, Is.EqualTo(value2));
            worker.Verify(_ => _.DoAsync(), Times.Exactly(2));
            reviewer.Verify(_ => _.Inspect(It.Is<int>(p => p == value1)), Times.Once);
            reviewer.Verify(_ => _.Inspect(It.Is<int>(p => p == value2)), Times.Never);
        }

        [Test]
        public void BasicCancelledFlow()
        {
            TaskCompletionSource<int> taskSource = new();
            Fresh<int> sut = new(() => taskSource.Task, _ => true);

            CancellationTokenSource cancellationTokenSource = new();
            Task<int> valueTask = sut.GetAsync(cancellationTokenSource.Token);
            cancellationTokenSource.Cancel();

            Assert.ThrowsAsync<TaskCanceledException>(async () => 
                await valueTask.SetTimeout(_timeout));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public async Task IndependentCancellationsFlow(int index)
        {
            const int value = 6;
            TaskCompletionSource<int> taskSource = new();
            CancellationTokenSource[] sources = new CancellationTokenSource[] { new(), new() };

            Fresh<int> sut = new(() => taskSource.Task, _ => true);

            Task<int>[] valueTasks = new[]
            {
                sut.GetAsync(sources[0].Token),
                sut.GetAsync(sources[1].Token),
            };

            sources[index].Cancel();
            Assert.ThrowsAsync<TaskCanceledException>(async () =>
                await valueTasks[index]);

            taskSource.SetResult(value);
            int result = await valueTasks[(index + 1) % 2];
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        [TestCaseSource(nameof(NotCancelledCancellationTokensParams))]
        public void BasicExceptionFlow(CancellationToken cancellationToken)
        {
            TaskCompletionSource<int> taskSource = new();

            Fresh<int> sut = new(() => taskSource.Task, _ => true);
            Task<int> valueTask = sut.GetAsync(cancellationToken);

            taskSource.SetException(new TestException());
            Assert.ThrowsAsync<TestException>(async () =>
                await valueTask);
        }

        [Test]
        [TestCaseSource(nameof(NotCancelledCancellationTokensParams))]
        public void TwoSimultaneousRequestsExceptionFlow(CancellationToken cancellationToken)
        {
            TaskCompletionSource<int> taskSource = new();

            Fresh<int> sut = new(() => taskSource.Task, _ => true);
            Task<int> valueTask1 = sut.GetAsync(cancellationToken);
            Task<int> valueTask2 = sut.GetAsync(cancellationToken);

            taskSource.SetException(new TestException());
            Assert.ThrowsAsync<TestException>(async () => await valueTask1);
            Assert.ThrowsAsync<TestException>(async () => await valueTask2);
        }

        [Test]
        [TestCaseSource(nameof(NotCancelledCancellationTokensParams))]
        public void TwoGradualRequestsExceptionFlow(CancellationToken cancellationToken)
        {
            const string Message1 = "A";
            const string Message2 = "B";

            Manager<TaskCompletionSource<int>> taskSourceManager = new();

            Fresh<int> sut = new(() => taskSourceManager.Current!.Task, _ => true);

            TaskCompletionSource<int> taskSource1 = taskSourceManager.Next(new());
            Task<int> valueTask1 = sut.GetAsync(cancellationToken);
            taskSource1.SetException(new TestException(Message1));

            TestException? resultEx1 = Assert.ThrowsAsync<TestException>(async () => await valueTask1);
            Assert.That(resultEx1, Is.Not.Null);
            Assert.That(resultEx1!.Message, Is.EqualTo(Message1));

            TaskCompletionSource<int> taskSource2 = taskSourceManager.Next(new());
            Task<int> valueTask2 = sut.GetAsync(cancellationToken);
            taskSource2.SetException(new TestException(Message2));

            TestException? resultEx2 = Assert.ThrowsAsync<TestException>(async () => await valueTask2);
            Assert.That(resultEx2, Is.Not.Null);
            Assert.That(resultEx2!.Message, Is.EqualTo(Message2));
        }
    }
}