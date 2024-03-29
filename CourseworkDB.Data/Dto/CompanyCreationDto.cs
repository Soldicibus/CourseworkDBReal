namespace CourseworkDB.Data.Dto;

public class CompanyCreationDto
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string CompanyEmail { get; set; }
    public string? CompanyPhone { get; set; }
    public int PublisherId { get; set; }
}
