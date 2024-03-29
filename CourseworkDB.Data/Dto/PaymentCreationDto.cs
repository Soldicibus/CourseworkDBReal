namespace CourseworkDB.Data.Dto;

public class PaymentCreationDto
{
    public int PaymentId { get; set; }
    public int AdGroupId { get; set; }
    public float PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; }
}
