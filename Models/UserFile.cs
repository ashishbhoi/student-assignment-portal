using System.ComponentModel.DataAnnotations;
using StudentClassworkPortal.Areas.Identity.Data;

namespace StudentClassworkPortal.Models;

public class UserFile
{
    public int Id { get; set; }

    [Required] public string FileName { get; set; } = default!;

    [Required] public byte[] Content { get; set; } = default!;

    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }

    public int? VirtualFolderId { get; set; }
    public VirtualFolder? VirtualFolder { get; set; }
}