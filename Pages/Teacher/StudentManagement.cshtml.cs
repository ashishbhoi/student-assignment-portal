using System.Globalization;
using System.Net;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentClassworkPortal.Areas.Identity.Data;

namespace StudentClassworkPortal.Pages.Teacher;

[Authorize(Roles = "Teacher")]
public class StudentManagementModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<StudentManagementModel> _logger;

    public StudentManagementModel(UserManager<ApplicationUser> userManager, IConfiguration configuration, ILogger<StudentManagementModel> logger)
    {
        _userManager = userManager;
        _configuration = configuration;
        _logger = logger;
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
            foreach (var userId in selectedStudents)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null) await _userManager.DeleteAsync(user);
            }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostPromoteSelectedAsync(string[] selectedStudents, StudentClass newClass,
        StudentSection newSection)
    {
        if (selectedStudents != null && selectedStudents.Length > 0)
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

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostBulkImportAsync(IFormFile bulkImportFile)
    {
        if (bulkImportFile != null && bulkImportFile.Length > 0)
        {
            try
            {
                using (var reader = new StreamReader(bulkImportFile.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.TypeConverterCache.AddConverter<StudentClass>(new StudentClassConverter());
                    var records = csv.GetRecords<StudentImportModel>().ToList();
                    
                    int successCount = 0;
                    int errorCount = 0;
                    var errorMessages = new List<string>();
                    
                    foreach (var record in records)
                    {
                        try
                        {
                            // Check if user already exists
                            var existingUser = await _userManager.FindByNameAsync(record.Username);
                            if (existingUser != null)
                            {
                                errorMessages.Add($"User '{WebUtility.HtmlEncode(record.Username)}' already exists.");
                                errorCount++;
                                continue;
                            }

                            var emailDomain = _configuration["AppSettings:StudentEmailDomain"] ?? "student.portal";
                            var user = new ApplicationUser
                            {
                                UserName = record.Username,
                                Email = record.Username + "@" + emailDomain,
                                Name = record.Name,
                                Class = record.Class,
                                Section = record.Section,
                                EmailConfirmed = true
                            };

                            var result = await _userManager.CreateAsync(user, record.Password);
                            if (result.Succeeded)
                            {
                                await _userManager.AddToRoleAsync(user, "Student");
                                successCount++;
                            }
                            else
                            {
                                errorCount++;
                                var userErrors = string.Join(", ", result.Errors.Select(e => e.Description));
                                errorMessages.Add($"Error creating user '{WebUtility.HtmlEncode(record.Username)}': {userErrors}");
                            }
                        }
                        catch (Exception ex)
                        {
                            errorCount++;
                            _logger.LogError(ex, "Error processing record for user '{Username}' during bulk import", record.Username);
                            errorMessages.Add($"Error processing record for '{WebUtility.HtmlEncode(record.Username)}'.");
                        }
                    }
                    
                    if (successCount > 0)
                    {
                        TempData["SuccessMessage"] = $"Successfully imported {successCount} student(s).";
                    }
                    if (errorCount > 0)
                    {
                        TempData["ErrorMessage"] = $"Failed to import {errorCount} student(s).";
                        TempData["ErrorDetails"] = errorMessages.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process CSV file during bulk import");
                TempData["ErrorMessage"] = "Failed to process the CSV file.";
                TempData["ErrorDetails"] = new string[] { $"Error: {ex.Message}" };
            }
        }
        else
        {
            TempData["ErrorMessage"] = "Please select a CSV file to upload.";
        }

        return RedirectToPage();
    }

    public IActionResult OnGetDownloadSampleCsv()
    {
        var builder = new StringBuilder();
        builder.AppendLine("Name,Username,Password,Class,Section");
        builder.AppendLine("John Doe,johndoe,Pa$$w0rd,X,A");
        builder.AppendLine("Jane Smith,janesmith,Pa$$w0rd,XI,B");
        builder.AppendLine("Bob Johnson,bobjohnson,Pa$$w0rd,XII,A");
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

public class StudentClassConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;

        // Handle direct enum values (VI, VII, VIII, IX, X, XI, XII)
        if (Enum.TryParse<StudentClass>(text, true, out var enumValue))
            return enumValue;

        // Handle numeric inputs (6, 7, 8, 9, 10, 11, 12)
        if (int.TryParse(text, out var numericValue))
        {
            return numericValue switch
            {
                6 => StudentClass.VI,
                7 => StudentClass.VII,
                8 => StudentClass.VIII,
                9 => StudentClass.IX,
                10 => StudentClass.X,
                11 => StudentClass.XI,
                12 => StudentClass.XII,
                _ => throw new TypeConverterException(this, memberMapData, text, row.Context, $"Unable to convert '{text}' to StudentClass. Valid values are: VI, VII, VIII, IX, X, XI, XII or 6, 7, 8, 9, 10, 11, 12")
            };
        }

        // Handle "Class" prefixed inputs (Class6, Class7, etc.)
        if (text.StartsWith("Class", StringComparison.OrdinalIgnoreCase))
        {
            var classNumber = text.Substring(5);
            if (int.TryParse(classNumber, out var classNum))
            {
                return classNum switch
                {
                    6 => StudentClass.VI,
                    7 => StudentClass.VII,
                    8 => StudentClass.VIII,
                    9 => StudentClass.IX,
                    10 => StudentClass.X,
                    11 => StudentClass.XI,
                    12 => StudentClass.XII,
                    _ => throw new TypeConverterException(this, memberMapData, text, row.Context, $"Unable to convert '{text}' to StudentClass. Valid class numbers are 6-12")
                };
            }
        }

        throw new TypeConverterException(this, memberMapData, text, row.Context, $"Unable to convert '{text}' to StudentClass. Valid values are: VI, VII, VIII, IX, X, XI, XII or 6, 7, 8, 9, 10, 11, 12 or Class6, Class7, etc.");
    }
}
