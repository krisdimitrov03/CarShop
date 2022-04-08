using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Core.Models
{
    public class UserEditViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "First Name cannot be empty.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name be empty.")]
        public string LastName { get; set; }
    }
}
