using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentClassworkPortal.Data;

namespace StudentClassworkPortal.Pages.Public
{
    public class ViewResourceModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ViewResourceModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int fileId)
        {
            var file = await _context.UserFiles
                .Include(f => f.VirtualFolder)
                .FirstOrDefaultAsync(f => f.Id == fileId);

            if (file == null || file.VirtualFolder == null || !file.VirtualFolder.IsPublicResource)
            {
                return NotFound();
            }

            return File(file.Content, "application/octet-stream", file.FileName);
        }
    }
}
