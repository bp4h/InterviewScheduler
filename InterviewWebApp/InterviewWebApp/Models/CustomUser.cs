using Microsoft.AspNetCore.Identity;

public class CustomUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<Interview> Interviews { get; set; }
    public List<ShareLink> ShareLinks { get; set; }
}

