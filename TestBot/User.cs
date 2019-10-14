using System.Collections.Generic;
using System.Threading.Tasks;

public class User
{
    public string fullUsername { get; set; }
    public string username { get; set; }
    public int? usedInvites { get; set; }
    public List<Invite> invites = new List<Invite>();
}