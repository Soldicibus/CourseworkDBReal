using System.ComponentModel.DataAnnotations;

namespace CourseworkDB.Data.Dto;

public class CompanyDto
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string CompanyEmail { get; set; }
    public string? CompanyPhone { get; set; }
}
