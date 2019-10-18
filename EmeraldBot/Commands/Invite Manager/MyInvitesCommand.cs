using Discord.Commands;
using System.Threading.Tasks;

public class MyInvitesCommand : CommandBase
{
    [Command("MyInvites")]
    public async Task MyInvites()
    {
        var user = Bot.GetUser(Context.User.ToString());

        await SendMessage($"{Context.User.Username.ToUpper()}", $"You have invited {user.inviteCount} members!");
    }
}

