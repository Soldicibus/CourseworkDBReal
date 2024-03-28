namespace CourseworkDB.Data.Dto;

public class PublisherDto
{
    public int PublisherId { get; set; }
    public UserDto User { get; set; }
    public string? WebsiteURL { get; set; }
}
