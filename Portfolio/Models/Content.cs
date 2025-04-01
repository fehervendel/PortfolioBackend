using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Models;

public class Content
{
    [Key]
    public int Id { get; init; }
    [Column(TypeName = "nvarchar(MAX)")]
    public string TextContent { get; set; }
    public string SectionId { get; set; }
    public int Order { get; set; }
}