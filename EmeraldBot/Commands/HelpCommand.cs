using Discord;
using Discord.Commands;
using System.Threading.Tasks;

public class HelpCommand : CommandBase
{
    [Command("Help")]
    public async Task Register(string minecraftUsername)
    {
        EmbedBuilder embedMessage = CreateEmbedMessage($"LIST OF COMMANDS", "$RegisterMinecraft (USERNAME) - Register your minecraft username." +
            "$Rewards - Shows a list of all your minecraft rewards." +
            "$Rewards USERNAME - Shows the rewards a specific user has." +
            "$Leaderboard - Shows the top 10 users with the most invites in the server." +
            "$Leaderboard # - Shows the top # users with the most invites in the server." +
            "$Claim - Claim your minecraft rewards" +
            "$MyInvites - Shows the number of invites that you have",
             Color.Blue);
        embedMessage.WithThumbnailUrl(Context.User.GetAvatarUrl());

        await Context.Channel.SendMessageAsync("", false, embedMessage.Build());
    }

}