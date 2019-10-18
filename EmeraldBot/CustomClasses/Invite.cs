/// <summary>
/// We've made this class to seperate the Discord invite logic (which we cannot control) into our own logic that we do have control over.
/// 
/// In this case we can see 3 types of uses: validUses, invalidUses, and manualUses:
/// validUses: Get incremented when a user accepts an invite and has never been on the server before
/// invalidUses: Get incremented when a user accepts an invite, but they have been to the server before
/// manuaUses: Get incremented by Admins of the server
/// 
/// We keep these separated because when we pull an Invite from Discord it simply has "Uses" - But we need to check this value in order to determine who accepted the invite.
/// 
/// Really what it comes down to is that Discord doesn't do any logic for checking if the invites are valid and this class is the framework to allow us to do just that.
/// </summary>

public class Invite
{
    public string inviteId { get; set; }
    public string username { get; set; }
    public int? validUses { get; set; }
    public int? invalidUses { get; set; }
    public int? manualUses { get; set; }
}