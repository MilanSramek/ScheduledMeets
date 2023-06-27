using ScheduledMeets.View;

namespace ScheduledMeets.Api;

[ExtendObjectType<MemberView>(IncludeStaticMembers = true)]
internal class MemberTypeExtension
{
    public  static ValueTask<string?> GetNicknameAsync(
        [Parent]MemberView member,
        [Service]IMemberExtender attendeeExtender,
        CancellationToken cancellationToken)
    {
        return attendeeExtender.GetNicknameAsync(member, cancellationToken);
    }
}