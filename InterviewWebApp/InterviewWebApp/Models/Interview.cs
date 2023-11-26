public class Interview
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Title { get; set; }

    public string UserId { get; set; }

    public CustomUser User { get; set; }
}
