﻿using System.ComponentModel.DataAnnotations;

namespace UrFUCoworkingsMicroservice.Models.DTOs
{
    public class UserEditModel
    {
        [Required(ErrorMessage = "Field \"Name\" couldn't be empty!")]
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
