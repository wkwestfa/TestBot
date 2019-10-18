using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ClaimCommand : CommandBase
{
    [Command("claim")]
    public async Task Claim()
    {
        var user = Bot.GetUser(Context.User.ToString());

        if (user.minecraftUsername == "")
        {
            await SendMessage("MINECRAFT NOT SETUP", $"You cannot claim your rewards because you have not yet set a minecraft username. {Environment.NewLine} {Environment.NewLine}" +
                                                     $"Please use the command '{Config.bot.cmdPrefix}registerMinecraft (USERNAME)' first.", Color.DarkRed);

            return;
        }

        if(user.minecraftRewards.Count == 0)
        {
            await SendMessage("NO REWARDS", "You do not currently have any rewards to claim.", Color.DarkRed);
            return;
        }

        await SendMessage("CHECKLIST BEFORE CONTINUING", 
                         $"1. Ensure you are logged into the minecraft server before claiming your rewards. {Environment.NewLine} {Environment.NewLine}" +
                         $"2. Ensure that your minecraft username is correct before continuing: **{user.minecraftUsername}**. { Environment.NewLine} { Environment.NewLine}" +
                         $"If both of these are correct, then use the command '{Config.bot.cmdPrefix}claim ready' to receive your rewards.", 
                         "You will lose these rewards FOREVER if either of these are incorrect.", 
                         Color.DarkRed);
    }

    [Command("claim")]
    public async Task Claim(string ready)
    {
        var user = Bot.GetUser(Context.User.ToString());

        if (user.minecraftRewards.Count == 0)
        {
            await SendMessage("NO REWARDS", "You do not currently have any rewards to claim.", Color.DarkRed);
            return;
        }

        if (ready.ToLower() == "ready")
        {
            foreach (var reward in user.minecraftRewards)
            {
                await SendMessage($"/give {user.minecraftUsername} {reward} 1");
            }

            user.minecraftRewards.Clear();

            Bot.UpdateUsersFile();
        }
    }
}