using Discord;
using Discord.Commands;
using System.Threading.Tasks;

public class MyRewardsCommand : CommandBase
{
    [Command("MyRewards")]
    public async Task Rewards()
    {
        var user = Bot.GetUser(Context.User.ToString());

        var embedMessage = CreateEmbedMessage($"REWARDS FOR {Context.User.Username.ToUpper()}", user.minecraftRewards, Color.Green);
        embedMessage.WithThumbnailUrl(Context.User.GetAvatarUrl());

        await SendMessage(embedMessage);
    }
}