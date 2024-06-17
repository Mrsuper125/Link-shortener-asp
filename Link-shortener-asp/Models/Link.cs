using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Link_shortener_asp.Models;

public class Link
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public string FullLink { get; set; }
    [DataType(DataType.Date)]
    public DateTime ExpireDate { get; set; }
    public int Clicks { get; set; }
}