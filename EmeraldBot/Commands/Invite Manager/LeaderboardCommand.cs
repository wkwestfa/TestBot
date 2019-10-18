using Discord;
using Discord.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
public class LeaderboardCommand : CommandBase
{
    [Command("leaderboard")]
    public async Task LeaderBoard()
    {
        int index = 0;
        int place = 1;

        List<string> leaderboard = new List<string>();
        User lastUser = new User();

        foreach (var user in Constants.users)
        {
            if (index >= 10)
                break;

            if (!TieBreaker(user, lastUser))
                place++;

            leaderboard.Add($"#{place} - {user.inviteCount} Invites - {user.username.Split('#')[0]}");

            lastUser = user;

            index++;
        }

        await SendMessage("TOP 10 - INVITE LEADERBOARD", leaderboard, Color.Orange);
    }

    [Command("leaderboard")]
    public async Task LeaderBoard(int numberOfUsers)
    {
        int index = 0;
        int place = 1;

        List<string> leaderboard = new List<string>();
        User lastUser = new User();

        foreach (var user in Constants.users)
        {
            if (index >= numberOfUsers)
                break;

            if (!TieBreaker(user, lastUser))    // If there is not a tiebreaker, then...
                place++;

            leaderboard.Add($"#{place} - {user.inviteCount} Invites - {user.username.Split('#')[0]}");

            lastUser = user;

            index++;
        }

        if(numberOfUsers > Constants.users.Count)
        {
            await SendMessage($"ENTIRE CHANNEL - INVITE LEADERBOARD", leaderboard, Color.Orange);
        }
        else
        {
            await SendMessage($"TOP {numberOfUsers} - INVITE LEADERBOARD", leaderboard, Color.Orange);
        }   
    }

    private bool TieBreaker(User firstUser, User secondUser)
    {
        if (secondUser.username == null || firstUser.username == null)   // If either users do not exist, then...
        {
            return true;    // Return true
        }

        return secondUser.inviteCount == firstUser.inviteCount; // Return if there is a tiebreaker or not.
    }
}