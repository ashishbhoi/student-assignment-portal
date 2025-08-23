using System.ComponentModel.DataAnnotations;
using StudentClassworkPortal.Areas.Identity.Data;

namespace StudentClassworkPortal.Models;

public class VirtualFolder
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Assignment Name")]
    public string AssignmentName { get; set; } = default!;

    [Required] public string Chapter { get; set; } = default!;

    [Required] public string Topic { get; set; } = default!;

    [Required] public StudentClass Class { get; set; }

    [Required] public StudentSection Section { get; set; }

    public bool IsPublicResource { get; set; } = false;

    public List<UserFile> UserFiles { get; set; } = new();
}