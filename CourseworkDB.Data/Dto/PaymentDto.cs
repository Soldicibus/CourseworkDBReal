namespace CourseworkDB.Data.Dto;

public class PaymentDto
{
    public int PaymentId { get; set; }
    public AdGroupsDto AdGroup { get; set; }
    public float PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; }
}