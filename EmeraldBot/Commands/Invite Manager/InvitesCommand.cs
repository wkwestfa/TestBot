using Discord;
using Discord.Commands;
using System.Threading.Tasks;

public class InvitesCommand : CommandBase
{
    [Command("Invites")]
    public async Task MyInvites(string username)
    {
        var user = Bot.GetUser(username.ToLower());

        await SendMessage($"INVITES", $"{user.username.Split('#')[0]} has invited {user.inviteCount} members!", Color.DarkBlue);
    }
}

