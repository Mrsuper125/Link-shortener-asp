using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Link_shortener_asp.Models;

public class Link
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public int Id { get; set; }

    [Required] public string FullLink { get; set; } = null!;
    [DataType(DataType.Date)] [Required] public DateTime ExpireDate { get; set; }
    [Required] public int Clicks { get; set; }
}