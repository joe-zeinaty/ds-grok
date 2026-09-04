using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using D_S_Grok.Data;
using D_S_Grok.Models;
using Microsoft.EntityFrameworkCore;

namespace D_S_Grok.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string email, string password)
        {
            if (_context.Users.Any(u => u.Email == email))
            {
                ViewBag.Error = "An account with this email already exists.";
                return View();
            }

            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = HashPassword(password),
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("UserID", user.UserID);
            HttpContext.Session.SetString("Username", user.Username);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            string hashedPassword = HashPassword(password);

            var user = _context.Users
                .FirstOrDefault(u => u.Email == email && u.PasswordHash == hashedPassword);

            if (user == null)
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }

            HttpContext.Session.SetInt32("UserID", user.UserID);
            HttpContext.Session.SetString("Username", user.Username);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        [HttpGet]
public IActionResult Profile()
{
    int? userId = HttpContext.Session.GetInt32("UserID");

    if (userId == null)
    {
        return RedirectToAction("Login", "Account");
    }

    var user = _context.Users.FirstOrDefault(u => u.UserID == userId);

    if (user == null)
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Account");
    }

    var achievementCount = _context.UserAchievements
        .Count(ua => ua.UserID == user.UserID);

    var model = new ProfileViewModel
    {
        Username = user.Username,
        TotalXP = user.TotalXP,
        CurrentLevel = user.CurrentLevel,
        AchievementCount = achievementCount
    };

    return View(model);
}

[HttpPost]
public IActionResult UpdateProfile(ProfileViewModel model)
{
    int? userId = HttpContext.Session.GetInt32("UserID");

    if (userId == null)
    {
        return RedirectToAction("Login", "Account");
    }

    var user = _context.Users.FirstOrDefault(u => u.UserID == userId);

    if (user == null)
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Account");
    }

    if (string.IsNullOrWhiteSpace(model.Username))
    {
        TempData["Error"] = "Username cannot be empty.";
        return RedirectToAction("Profile");
    }

    bool usernameExists = _context.Users
        .Any(u => u.Username == model.Username && u.UserID != user.UserID);

    if (usernameExists)
    {
        TempData["Error"] = "This username is already taken.";
        return RedirectToAction("Profile");
    }

    user.Username = model.Username.Trim();

    _context.SaveChanges();

    TempData["Success"] = "Profile updated successfully.";

    return RedirectToAction("Profile");
}

[HttpPost]
public IActionResult ChangePassword(ProfileViewModel model)
{
    int? userId = HttpContext.Session.GetInt32("UserID");

    if (userId == null)
    {
        return RedirectToAction("Login", "Account");
    }

    var user = _context.Users.FirstOrDefault(u => u.UserID == userId);

    if (user == null)
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Account");
    }

    if (string.IsNullOrWhiteSpace(model.CurrentPassword) ||
        string.IsNullOrWhiteSpace(model.NewPassword) ||
        string.IsNullOrWhiteSpace(model.ConfirmNewPassword))
    {
        TempData["Error"] = "Please fill in all password fields.";
        return RedirectToAction("Profile");
    }

    if (model.NewPassword != model.ConfirmNewPassword)
    {
        TempData["Error"] = "New passwords do not match.";
        return RedirectToAction("Profile");
    }

    if (model.NewPassword.Length < 6)
    {
        TempData["Error"] = "New password must be at least 6 characters.";
        return RedirectToAction("Profile");
    }

    // IMPORTANT:
    // Use the same password logic you used in Register/Login.
    // If your project currently stores plain text passwords, replace this
    // with your existing comparison style for now.

    if (user.PasswordHash != model.CurrentPassword)
    {
        TempData["Error"] = "Current password is incorrect.";
        return RedirectToAction("Profile");
    }

    user.PasswordHash = model.NewPassword;

    _context.SaveChanges();

    TempData["Success"] = "Password changed successfully.";

    return RedirectToAction("Profile");
}
    }
}