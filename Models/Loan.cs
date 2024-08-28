using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onboardingAPI.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string? Tenure { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public LoanHistory? LoanHistory { get; set; }
        public ICollection<LoanPayment> LoanPayment { get; set; } = new List<LoanPayment>();

    }

    public class LoanHistory
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? AppUserId { get; set; }
        public int LoanId { get; set; }
        public Loan? Loan { get; set; }

    }
    public class LoanPayment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string? TransID { get; set; }
        public string? Channel { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? AppUserId { get; set; }
        public int LoanId { get; set; }
        public Loan? Loan { get; set; }
    }
}