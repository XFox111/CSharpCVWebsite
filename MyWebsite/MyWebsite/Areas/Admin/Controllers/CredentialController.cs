using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Controllers;
using MyWebsite.Helpers;
using MyWebsite.Models.Databases;
using System.Linq;

namespace MyWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CredentialController : ExtendedController
    {
        const string viewPath = "Areas/Admin/Views/Shared/Credential.cshtml";

        public CredentialController(DatabaseContext context) : base(context) { }

        [HttpGet]
        public IActionResult Index() =>
            View(viewPath);

        [HttpPost]
        public IActionResult Index(Models.CredentialModel model)
        {
            MyWebsite.Models.CredentialModel credential = Database.Users.FirstOrDefault();

            if (model == null || model.Current.Email != credential.Email || !Encryptor.VerifyHash(model.Current.Password, credential.Password))
            {
                ModelState.AddModelError("Authorization error", "Invaild e-mail or password");
                return View(viewPath, model);
            }

            if(string.IsNullOrWhiteSpace(model.Updated.Email) && string.IsNullOrWhiteSpace(model.Current.Password))
            {
                ModelState.AddModelError("Validation error", "No data to update");
                return View(viewPath, model);
            }

            Database.Users.Remove(credential);
            Database.SaveChanges();
            if(!string.IsNullOrWhiteSpace(model.Current.Email))
                credential.Email = model.Updated.Email;
            if(!string.IsNullOrWhiteSpace(model.Current.Password))
                credential.Password = Encryptor.ComputeHash(model.Updated.Password);
            Database.Users.Add(credential);

            Database.SaveChanges();

            return Redirect("~/Admin/Logout");
        }
    }
}