using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models;

public class Card
{
    [Key]
    public int Id { get; init; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
    public int Order { get; set; }
}