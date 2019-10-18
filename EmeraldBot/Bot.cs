using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public static class Bot
{
    static JObject jsonUsers = new JObject();

    public static void WriteToFile(string path)
    {
        // Create a file to write to.
        string createText = "Hello and Welcome" + Environment.NewLine;
        File.WriteAllText(path, createText);

        // Open the file to read from.
        string readText = File.ReadAllText(path);
    }

    public static void UpdateUsersFile()
    {
        var users = Constants.users.OrderByDescending(x => x.inviteCount).ToList();

        // serialize JSON directly to a file
        using (StreamWriter file = File.CreateText(@"Data/users.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;

            serializer.Serialize(file, users);
        }
    }

    public static void UpdateInvitesFile()
    {
        var invites = Constants.invites.OrderByDescending(x => x.validUses).ToList();

        // serialize JSON directly to a file
        using (StreamWriter file = File.CreateText(@"Data/invites.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;

            serializer.Serialize(file, invites);
        }
    }

    /// <summary>
    /// Convert SocketGuildUser (Discord) to our custom User class and add to our user list.
    /// </summary>
    public static void AddUser(SocketGuildUser user)
    {
        Constants.users.Add(new User
        {
            username = user.ToString(),
            shortUsername = user.Username,
            inviteCount = 0,
            minecraftUsername = "",
            minecraftRewards = new List<string>(),
        });
    }

    /// <summary>
    /// Convert RestInviteMetadata (AKA: An invite made on discord) into our own custom invite to keep track of.
    /// </summary>
    public static void AddInvite(RestInviteMetadata invite)
    {
        Constants.invites.Add(new Invite
        {
            inviteId = invite.Id,
            username = invite.Inviter.ToString(),
            validUses = invite.Uses,
            invalidUses = 0,
            manualUses = 0
        });
    }

    public static User GetUser(string userName)
    {
        return Constants.users.Find(x => x.username.Contains(userName));
    }

    public static List<User> GetUsersFromFile()
    {
        List<User> users = new List<User>();

        using (StreamReader reader = new StreamReader(@"Data/users.json"))
        {
            string jsonString = reader.ReadToEnd();

            users = JsonConvert.DeserializeObject<List<User>>(jsonString);
        }

        return users;
    }

    public static List<Invite> GetInvitesFromFile()
    {
        List<Invite> invites = new List<Invite>();

        using (StreamReader reader = new StreamReader(@"Data/invites.json"))
        {
            string jsonString = reader.ReadToEnd();

            invites = JsonConvert.DeserializeObject<List<Invite>>(jsonString);
        }

        return invites;
    }

    public static List<Reward> GetRewardsFromFile()
    {
        List<Reward> rewards = new List<Reward>();

        string[] lines = File.ReadAllLines(@"Data/MinecraftRewards.txt");
        
        foreach (string line in lines)
        {
            rewards.Add(new Reward
            {
                invitesRequired = Convert.ToInt16(line.Split(',')[0]),
                reward = line.Split(',')[1]
            });
        }

        return rewards;
    }

    public static void AddRewards(User user)
    {
        foreach (var reward in Constants.rewards)
        {
            if(reward.invitesRequired == user.inviteCount)
            {
                user.minecraftRewards.Add(reward.reward);
            }
        }
    }

    public static async Task UpdateUserName(SocketUser oldUser, SocketUser updatedUser)
    {
        GetUser(oldUser.Username).username = updatedUser.ToString();
        GetUser(oldUser.Username).shortUsername = updatedUser.Username;

        UpdateUsersFile();
    }
}

