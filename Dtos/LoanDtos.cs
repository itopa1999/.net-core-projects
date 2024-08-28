using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace onboardingAPI.Dtos
{
    public class LoanDtos
    {
        public decimal Amount { get; set; }
    }

    public class CreateLoanDtos
    {
        [Required]
        public decimal Amount { get; set; }
        
        [Required]
        public string? Tenure { get; set; }
    }

    public class repaymentDto
    {
        [Required]
        public decimal Amount { get; set; }
    }
}