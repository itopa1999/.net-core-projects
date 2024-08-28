using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using onboardingAPI.Models;

namespace onboardingAPI.Interfaces
{
    public interface ILoanRepository
    {
        Task<Loan> CreateLoanAsync(Loan createLoan);
        Task<Loan?> GetLoanBalanceAsync(string userId);
        Task<Loan?> LoanRepaymentAsync(string userId, decimal Amount);
    }
}