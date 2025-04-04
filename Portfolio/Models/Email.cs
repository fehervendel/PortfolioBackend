using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Models;

public class Email
{
    [Key]
    public int Id { get; init; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
    [Column(TypeName = "nvarchar(MAX)")]
    public string Message { get; set; }
}