namespace ScheduledMeets.View;

internal sealed class MemberExtender : IMemberExtender
{
    private readonly IWithIdReader<UserView> _userReader;

    public MemberExtender(IWithIdReader<UserView> userReader)
    {
        _userReader = userReader ?? throw new ArgumentNullException(nameof(userReader));
    }

    public async ValueTask<string?> GetNicknameAsync(MemberView member,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(member);

        if (member is { Nickname: string nickname })
            return nickname;

        UserView user = await _userReader.ReadAsync(member.UserId, cancellationToken);
        return user.Nickname;
    }
}