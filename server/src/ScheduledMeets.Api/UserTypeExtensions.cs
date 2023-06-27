using ScheduledMeets.View;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledMeets.Api;

[ExtendObjectType<UserView>]
internal sealed class UserTypeExtensions
{
    public Task<MeetView> GetCurrentMeets([Parent]UserView user)
    {

        ArgumentNullException.ThrowIfNull(user);

        throw new NotImplementedException();
    }
}
