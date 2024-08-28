using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using onboardingAPI.Dtos;
using onboardingAPI.Models;

namespace onboardingAPI.Mappers
{
    public static class LoanMappers
    {
        public static Loan ToCreateLoanDto(this CreateLoanDtos createLoan){
            return new Loan{
                Amount = createLoan.Amount,
                Tenure = createLoan.Tenure
            };
        }
    }
}