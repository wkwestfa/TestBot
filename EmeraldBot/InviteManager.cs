using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public static class InviteManager
{
    private static SocketGuild channel;
    private static bool existingUser = false;

    private static List<RestInviteMetadata> discordInvites;

    /// <summary>
    /// Runs everytime the bot starts - Sets the Discord channel it is looking at.
    /// </summary>
    /// <param name="client"></param>
    public static async Task BootUp(DiscordSocketClient client)
    {
        channel = client.GetGuild(214101582654996480);

        if (!File.Exists("Data/Users.json"))  // If the file that contains all the user data does not exist, then..
            await Init();                     // The bot has to be initialized.

        Constants.rewards = Bot.GetRewardsFromFile();
        Constants.users = Bot.GetUsersFromFile();
        Constants.invites = Bot.GetInvitesFromFile();
    }

    /// <summary>
    /// Only runs if the proper files have not been created (user) and creates the files for us.
    /// </summary>
    public static async Task Init()
    {
        var rawInvites = await channel.GetInvitesAsync();

        foreach (var user in channel.Users)
        {
            Bot.AddUser(user);
        }

        foreach (var invite in rawInvites)
        {
            Bot.AddInvite(invite);
        }

        foreach (var user in Constants.users)
        {
            var userInvites = Constants.invites.FindAll(x => x.username == user.username);

            if (userInvites.Count > 0)
            {
                foreach (var invite in userInvites)
                {
                    user.inviteCount += invite.validUses;
                }
            }

            // Add rewards to users
            string[] lines = File.ReadAllLines(@"Data/MinecraftRewards.txt");

            foreach (string line in lines)
            {
                var rewardLevel = line.Split(',')[0];
                var reward = line.Split(',')[1];

                if (user.inviteCount >= Convert.ToInt16(rewardLevel))
                {
                    user.minecraftRewards.Add(reward);
                }
            }
        }

        Bot.UpdateInvitesFile();
        Bot.UpdateUsersFile();
    }

    /// <summary>
    /// Runs every time a new user joins the server.
    /// </summary>
    public static async Task NewUserJoined(SocketGuildUser newUser)
    {
        // Discord API doesn't make our jobs easy, so we have to grab the data from the API and then convert it into a list that was can manipulate ourselves, hence the two lines below.
        var rawDiscordInvites = await channel.GetInvitesAsync();
        discordInvites = new List<RestInviteMetadata>(rawDiscordInvites);

        CalculateNewInvites();

        if (Bot.GetUser(newUser.ToString()) == null)    // If the user has never been to the server before, then...
        {
            Bot.AddUser(newUser);                       // Add them to our user list.
            existingUser = false;
        } 
        else                        // Otherwise, if the user HAS joined the server in the past, then...
        {
            existingUser = true;    // We make sure our code is aware of that as we will need to know later.
            Console.WriteLine($"{newUser.Username} joined the server, but they have been here in the past.");
        }   

        CalculateValidInvite(newUser);

        Bot.UpdateUsersFile();
        Bot.UpdateInvitesFile();
    }

    /// <summary>
    /// Find out which invites are new and add them to our file.
    /// </summary>
    private static void CalculateNewInvites()
    {
        var newInvites = discordInvites.Where(x => Constants.invites.Any(y => y.inviteId == x.Id) == false).ToList(); // Determine which invites are new.

        foreach (var invite in newInvites)
        {
            Console.WriteLine($"{invite.Inviter.Username} created a new invite!");

            Constants.invites.Add(new Invite
            {
                inviteId = invite.Id,
                username = invite.Inviter.Username,
                validUses = invite.Uses,
                manualUses = 0
            });
        }
    }

    /// <summary>
    /// Invites have two integer values: validUses and invalidUses.
    /// This function determines which one of those values should be incremented.
    /// </summary>
    private static void CalculateValidInvite(SocketGuildUser newUser)
    {
        foreach (var discordInvite in discordInvites)   // Cycle through discord invites
        {
            foreach (var invite in Constants.invites)             // For each discord invite, we also need to cycle through our custom invites to determine the correct one.
            {
                if (invite.inviteId == discordInvite.Id && (invite.validUses + invite.invalidUses) < discordInvite.Uses)    // if the id's are the same and the uses are less than the Discord Uses, then we know this is the invite!
                {
                    if (!existingUser)  // Now we check to make sure the user has never been on the server before, and then...
                    {
                        Bot.GetUser(discordInvite.Inviter.ToString()).inviteCount += 1; // We can finally add an invite to the person who invited them!
                        invite.validUses += 1;
                        Bot.AddRewards(Bot.GetUser(discordInvite.Inviter.ToString()));  // We must add rewards to the user who invited the new member.

                        Console.WriteLine($"{newUser.Username} joined for the first time!  Invited by {invite.username.Split('#')[0]}. {invite.username.Split('#')[0]} has invited {Bot.GetUser(discordInvite.Inviter.ToString()).inviteCount} new members in total!");
                        break;
                    }
                    else    // But if the user has already been to the server before, then...
                    {
                        invite.invalidUses += 1;    // We know it is not a proper invite.
                        break;
                    }
                }
                else if (invite.inviteId == discordInvite.Id)
                {
                    break;
                }
            }
        }
    }
}