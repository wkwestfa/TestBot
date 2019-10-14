using Discord.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SendMessageCommand : CommandBase
{
    private const string configFolder = "Reso";
    private const string configFile = "users.json";

    [Command("SendMessage")]
    public async Task Message()
    {
        List<User> users = new List<User>();
        List<User> usersWithInvites = new List<User>();

        foreach (var user in Context.Guild.Users)
        {
            users.Add(new User()
            {
                fullUsername = user.ToString(),
                username = user.Username,
                usedInvites = 0,
            });
        }

        var rawInvites = await Context.Guild.GetInvitesAsync();
        List<Invite> invites = new List<Invite>();

        foreach (var invite in rawInvites)
        {
            invites.Add(new Invite
            {
                username = invite.Inviter.ToString(),
                inviteUrl = invite.Url,
                timesUsed = invite.Uses,
            });
        }

        foreach (var user in users)
        {
            var userInvites = invites.FindAll(x => x.username == user.fullUsername);

            if(userInvites.Count > 0)
            {
                foreach (var invite in userInvites)
                {
                    user.invites.Add(invite);
                    user.usedInvites += invite.timesUsed;
                }
            }
        }

        usersWithInvites.AddRange(users.FindAll(x => x.invites.Count > 0));

        await SendMessage("This is a message");
    }
}