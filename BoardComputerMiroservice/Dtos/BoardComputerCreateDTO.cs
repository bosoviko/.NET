using System.ComponentModel.DataAnnotations;

namespace BoardComputerMiroservice.Dtos
{
    public class BoardComputerCreateDTO
    {
        [Required] public string Name { get; set; }
        [Required] public string Company { get; set; }
        [Required] public string Model { get; set; }
        [Required] public int SerialNumber { get; set; }
        [Required] public float Memory { get; set; }
    }
}
