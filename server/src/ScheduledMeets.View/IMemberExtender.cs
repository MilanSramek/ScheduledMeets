namespace ScheduledMeets.View;

public interface IMemberExtender
{
    ValueTask<string?> GetNicknameAsync(MemberView member, CancellationToken cancellationToken);
}