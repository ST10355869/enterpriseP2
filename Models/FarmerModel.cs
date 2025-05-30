﻿using System.ComponentModel.DataAnnotations;
namespace enterpriseP2.Models
{
    public class FarmerModel
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public string Role { get; set; } = "Farmer";
    }

}
