using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseworkDB.Data.Models;

[Table("Payments")]
public class Payment
{
    [Key]
    [Required]
    public int PaymentId { get; set; }
    public AdGroup AdGroup { get; set; }
    [Required]
    public float PaymentAmount { get; set; }
    [Required]
    public DateTime PaymentDate { get; set; }
}
