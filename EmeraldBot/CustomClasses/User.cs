using System.Collections.Generic;
/// <summary>
/// This class is necessary to:
///     1. Keep track of users who have been on the server in the past.
///     2. Keep track of all the users minecraft username.
///     3. Add up all uses from all invites associated to the user and keep them in one central location to pull from.
/// </summary>
public class User
{
    public string username { get; set; }
    public string shortUsername { get; set; }   // Username for discord that doesn't have the numbers at the end.
    public string minecraftUsername { get; set; }
    public int? inviteCount { get; set; }
    public List<string> minecraftRewards { get; set; }
}