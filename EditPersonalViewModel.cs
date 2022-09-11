using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestFinal.ViewModels
{
    public class EditPersonalViewModel 
    {
		public string Id { get; set; }
		public string ProfilePicture { get; set; }


		[DataType(DataType.Upload)]
		public IFormFile ProfilePhoto { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string NumberPhone { get; set; }

		public string Oras { get; set; }

	}
}
