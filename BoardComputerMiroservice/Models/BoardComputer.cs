using System.ComponentModel.DataAnnotations;

namespace BoardComputerMiroservice.Models
{
    public class BoardComputer
    {
        [Key]
        [Required] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Company { get; set; }
        [Required] public string Model { get; set; }
        [Required] public int SerialNumber { get; set; }
        [Required] public float Memory { get; set; }
    }
}
