using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using TestFinal.Data;
using TestFinal.Models;
using TestFinal.Models.Repositores;
using TestFinal.ViewModels;

namespace TestFinal.Controllers
{
    public class ContactController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;

        private readonly IContactService contactService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly MyDbContext context;
        public UserManager<ApplicationUser> UserManager { get; }

        public ContactController(IWebHostEnvironment _hostingEnvironment, IContactService _contactService,
            RoleManager<IdentityRole> _roleManager,
               UserManager<ApplicationUser> _userManager , MyDbContext _myDbContext)
        {
            hostingEnvironment = _hostingEnvironment;
            contactService = _contactService;
            roleManager = _roleManager;
            userManager = _userManager;
            context = _myDbContext;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult ContactList()
        {
            var contact = contactService.ListAllContact();
            return View("ListContact", contact);
        }

        [HttpGet]
        public IActionResult CreateContact()
        {
            return View("CreateContact");
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactViewModel model)
        {
            if (ModelState.IsValid)
            {

                Contact contact = new Contact
                {
                    Name = model.Name,
                    Email = model.Email,
                    Message = model.Message

                };

                MailMessage mail = new MailMessage();
                mail.To.Add(model.Email);
                mail.From = new MailAddress("antonieraduemilian@gmail.com");
                mail.Subject = "Echipa E-Thermal";
                mail.Body = "Salut! Am primit email-ul tau si cat de curand o sa te contactam!";

                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("antonieraduemilian@gmail.com", "qxsutyzjblnpdscm");
                smtp.EnableSsl = true;
                smtp.Send(mail);

                await contactService.AddContactAsync(contact);

                return RedirectToAction("Index", "home");
            }

            return View("CreateContact", model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await contactService.GetContactByIdAsync(id);

            await contactService.DeleteContactAsync(contact);

            return RedirectToAction("ContactList", "contact");
        }

        [HttpGet]
        public async Task<IActionResult> Response(int id)
        {
            var contact = context.Contacts.Where(i => i.ContactId == id).FirstOrDefault();
            var model = new CreateResponseViewModel
            {
                Id = contact.ContactId,
                Email = contact.Email
            };
            return View("Response", model);

        }
        [HttpPost]
        public async Task<IActionResult> Response(CreateResponseViewModel model)
        {

            var contact = contactService.GetContactByIdAsync(model.Id);

            MailMessage mail = new MailMessage();
            mail.To.Add(contact.Result.Email);
            mail.From = new MailAddress("antonieraduemilian@gmail.com");
            mail.Subject = model.Subject;
            mail.Body = model.Message;

            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("antonieraduemilian@gmail.com", "qxsutyzjblnpdscm");
            smtp.EnableSsl = true;
            smtp.Send(mail);

            return RedirectToAction("ContactList");

        }
    }
}
