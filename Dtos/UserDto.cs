using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace onboardingAPI.Dtos
{
    public class UserDto
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? HouseNo { get; set; }
        [Required]
        public string? StreetName { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? LGA { get; set; }
        [Required]
        public string? AccountType { get; set; }
    }

    public class CreateUserDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? HouseNo { get; set; }
        [Required]
        public string? StreetName { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? LGA { get; set; }
        [Required]
        public string? AccountType { get; set; }
    }

    public class VerifyTokenDto
    {
        [Required]
        public int Token { get; set; }
    }

    public class ResendTokenDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }

    public class BvnVerificationDto
    {
        [Required]
        public int BvnNumber { get; set; }
        // [Required]
        // public DateOnly DOB { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }


    public class BusinessInfoDto
    {
        [Required]
        public string? BusinessName { get; set; }
        [Required]
        public string? BusinessCategory { get; set; }
        public string? HouseNo { get; set; }
        [Required]
        public string? StreetName { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? LGA { get; set; }
        [Required]
        public string? BusinessType { get; set; }
        [Required]
        public string? RegNumber { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }

    public class IdentityInfoDto{
        [Required]
        public string? IdentityType { get; set; }
        [Required]
        public string? IdentityNumber { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }

    public class LoginDto{
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }

    }
}