using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentClassworkPortal.Data;
using StudentClassworkPortal.Models;

namespace StudentClassworkPortal.Pages.Teacher;

[Authorize(Roles = "Teacher")]
public class UploadToPublicFolderModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public UploadToPublicFolderModel(ApplicationDbContext context)
    {
        _context = context;
        Folder = new VirtualFolder();
    }

    [BindProperty] public VirtualFolder Folder { get; set; }

    public async Task<IActionResult> OnGetAsync(int folderId)
    {
        var folder = await _context.VirtualFolders
            .Include(vf => vf.UserFiles)
            .FirstOrDefaultAsync(vf => vf.Id == folderId && vf.IsPublicResource);

        if (folder == null) return NotFound();

        Folder = folder;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(IFormFile fileUpload, int folderId, string newFileName)
    {
        if (fileUpload == null || fileUpload.Length == 0 || string.IsNullOrWhiteSpace(newFileName))
            // Ideally, add a model state error here to inform the user.
            return RedirectToPage(new {folderId});

        var folder = await _context.VirtualFolders.FindAsync(folderId);
        if (folder == null || !folder.IsPublicResource) return NotFound();

        using var memoryStream = new MemoryStream();
        await fileUpload.CopyToAsync(memoryStream);

        // Get the original file extension
        var originalFileName = Path.GetFileName(fileUpload.FileName);
        var fileExtension = Path.GetExtension(originalFileName);

        var newFile = new UserFile
        {
            FileName = $"{newFileName}{fileExtension}",
            Content = memoryStream.ToArray(),
            VirtualFolderId = folderId
        };

        _context.UserFiles.Add(newFile);
        await _context.SaveChangesAsync();

        return RedirectToPage(new {folderId});
    }

    public async Task<IActionResult> OnPostDeleteFileAsync(int fileId, int folderId)
    {
        var file = await _context.UserFiles.FindAsync(fileId);
        if (file != null)
        {
            _context.UserFiles.Remove(file);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage(new {folderId});
    }
}