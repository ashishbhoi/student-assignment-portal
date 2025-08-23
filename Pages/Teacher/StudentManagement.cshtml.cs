
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

using System.IO;
using System.Globalization;
using CsvHelper;
using Microsoft.AspNetCore.Http;

using System.Text;

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

        public async Task<IActionResult> OnPostBulkImportAsync(IFormFile bulkImportFile)
        {
            if (bulkImportFile != null && bulkImportFile.Length > 0)
            {
                using (var reader = new StreamReader(bulkImportFile.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<StudentImportModel>();
                    foreach (var record in records)
                    {
                        var user = new ApplicationUser
                        {
                            UserName = record.Username,
                            Email = record.Username + "@example.com",
                            Name = record.Name,
                            Class = record.Class,
                            Section = record.Section,
                            EmailConfirmed = true
                        };

                        var result = await _userManager.CreateAsync(user, record.Password);
                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, "Student");
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                    }
                }
            }

            return RedirectToPage();
        }

        public IActionResult OnGetDownloadSampleCsv()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Name,Username,Password,Class,Section");
            builder.AppendLine("John Doe,johndoe,Password123,Class10,A");
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "sample-students.csv");
        }

        public class StudentImportModel
        {
            public string Name { get; set; } = string.Empty;
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public StudentClass Class { get; set; }
            public StudentSection Section { get; set; }
        }
    }
}
