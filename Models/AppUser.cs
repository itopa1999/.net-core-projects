using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace onboardingAPI.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? HouseNo { get; set; }
        public string? StreetName { get; set; }
        public string? State { get; set; }
        public string? LGA { get; set; }
        public string? AccountType { get; set; }
        public int SponsorCode { get; set; }
        public int Pin { get; set; }
        public int AccountNumber { get; set; }
        public bool IsActive { get; set; } = false;
        public bool Verified { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime VerifiedAt { get; set; }


        // for navigation
        public Otp? Otp { get; set; }
        public BVNInfo? BVNInfo { get; set; }
        public BusinessInfo? BusinessInfo { get; set; }
        public IDInfo? IDInfo { get; set; }
        public UserActions? UserAction { get; set; }
        public Loan? Loan { get; set; }
        public LoanHistory? LoanHistory { get; set; }
        public LoanPayment? LoanPayment { get; set; }
    }

    public class Otp
    {
        public int Id { get; set; }
        public int Token { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }

    public class BVNInfo
    {
        public int Id { get; set; }
        public int BvnNumber { get; set; }
        public DateOnly DOB { get; set; } 
        public string? Gender { get; set; }
        public bool Verified { get; set; } = false;
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        

    }

    public class BusinessInfo
    {
        public int Id { get; set; }
        public string? BusinessName { get; set; }
        public string? BusinessCategory { get; set; }
        public string? HouseNo { get; set; }
        public string? StreetName { get; set; }
        public string? State { get; set; }
        public string? LGA { get; set; }
        public string? BusinessType { get; set; }
        public string? RegNumber { get; set; }
        public bool Verified { get; set; } = false;
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
    }

    public class IDInfo
    {
        public int Id { get; set; }
        public string? IdentityType { get; set; }
        public string? IdentityNumber { get; set; }
        public bool Verified { get; set; } = false;
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}