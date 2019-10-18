using Discord;
using Discord.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RewardsCommand : CommandBase
{
    [Command("Rewards")]
    public async Task Rewards()
    {
        List<string> rewards = new List<string>();

        foreach (var reward in Constants.rewards)
        {
            rewards.Add($"({reward.invitesRequired}) {reward.reward}");
        }

        await SendMessage("LIST OF REWARDS", rewards, Color.Blue);
    }

    [Command("Rewards")]
    public async Task Rewards(string username)
    {
        var user = Bot.GetUser(username);

        var embedMessage = CreateEmbedMessage($"REWARDS FOR {username.ToUpper()}", user.minecraftRewards, Color.Green);
        embedMessage.WithThumbnailUrl(Context.User.GetAvatarUrl());

        await SendMessage(embedMessage);
    }
}