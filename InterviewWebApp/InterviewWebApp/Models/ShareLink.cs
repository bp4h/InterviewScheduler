public class ShareLink
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public Guid Guid { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }

    public CustomUser User { get; set; }
}
