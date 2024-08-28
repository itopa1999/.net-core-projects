using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using onboardingAPI.Data;
using onboardingAPI.Interfaces;
using onboardingAPI.Models;

namespace onboardingAPI.Repository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IJwtRepository _token;
        public LoanRepository(ApplicationDBContext context, IJwtRepository token)
        {
            _context = context;
            _token = token;
        }
        public async Task<Loan> CreateLoanAsync(Loan createLoan)
        {
            await _context.Loans.AddAsync(createLoan);
            await _context.SaveChangesAsync();
            return createLoan;
            
            
        }

        public async Task<Loan?> GetLoanBalanceAsync(string userId)
        {
           var loan= await _context.Loans.FirstOrDefaultAsync(x=>x.AppUserId == userId);
           return loan; 
        }

        public async Task<Loan?> LoanRepaymentAsync(string userId, decimal Amount)
        {
           var loan = await _context.Loans.FirstOrDefaultAsync(x=>x.AppUserId == userId && x.Amount > 0);
           loan.Amount -= Amount;
           var transaction = new LoanPayment{
            Amount = Amount,
            AppUserId = userId,
            LoanId = loan.Id,
            TransID = _token.GenerateTransactionID(),
            Channel = $"Repayment of {Amount} was successful",
           };
           await _context.LoanPayments.AddAsync(transaction);
           await _context.SaveChangesAsync();
           return loan; 
        }
    }
}