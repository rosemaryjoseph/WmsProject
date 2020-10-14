using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessService.Domain.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PassWord { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PAN { get; set; }
        [Required]
        [Phone]
        public string ContactNo { get; set; }
        public DateTime DOB { get; set; }

        public string AccountType { get; set; }

        public Boolean IsDeleted { get; set; }

    }
}

