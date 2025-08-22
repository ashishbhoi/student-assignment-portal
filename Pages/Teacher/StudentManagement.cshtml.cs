
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentClassworkPortal.Areas.Identity.Data;
using StudentClassworkPortal.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentClassworkPortal.Pages.Teacher
{
    [Authorize(Roles = "Teacher")]
    public class StudentManagementModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentManagementModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            StudentsByClass = new Dictionary<StudentClass, Dictionary<StudentSection, List<ApplicationUser>>>();
        }

        public Dictionary<StudentClass, Dictionary<StudentSection, List<ApplicationUser>>> StudentsByClass { get; set; }

        public async Task OnGetAsync()
        {
            var students = await _userManager.GetUsersInRoleAsync("Student");
            var groupedStudents = students
                .Where(s => s.Class.HasValue && s.Section.HasValue)
                .GroupBy(s => s.Class!.Value)
                .ToDictionary(
                    classGroup => classGroup.Key,
                    classGroup => classGroup
                        .GroupBy(s => s.Section!.Value)
                        .ToDictionary(
                            sectionGroup => sectionGroup.Key,
                            sectionGroup => sectionGroup.ToList()
                        )
                );

            StudentsByClass = groupedStudents;
        }

        public async Task<IActionResult> OnPostDeleteSelectedAsync(string[] selectedStudents)
        {
            if (selectedStudents != null && selectedStudents.Length > 0)
            {
                foreach (var userId in selectedStudents)
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        await _userManager.DeleteAsync(user);
                    }
                }
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostPromoteSelectedAsync(string[] selectedStudents, StudentClass newClass, StudentSection newSection)
        {
            if (selectedStudents != null && selectedStudents.Length > 0)
            {
                foreach (var userId in selectedStudents)
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        user.Class = newClass;
                        user.Section = newSection;
                        await _userManager.UpdateAsync(user);
                    }
                }
            }

            return RedirectToPage();
        }
    }
}
