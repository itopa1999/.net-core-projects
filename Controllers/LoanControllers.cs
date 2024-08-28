using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using onboardingAPI.Data;
using onboardingAPI.Dtos;
using onboardingAPI.Interfaces;
using onboardingAPI.Mappers;
using onboardingAPI.Models;

namespace onboardingAPI.Controllers
{
    [Route("api/loan")]
    [ApiController]
    public class LoanControllers : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ILoanRepository _loanRepo;
        public LoanControllers(ApplicationDBContext context, ILoanRepository loanRepo)
        {
            _context = context;
            _loanRepo = loanRepo;
        }

        [HttpPost("apply")]
        [Authorize]
        public async Task<IActionResult> ApplyLoan([FromBody] CreateLoanDtos createLoan){
            try{
                if (!ModelState.IsValid){
                return BadRequest(ModelState);
                }
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Console.WriteLine(userId);
                var activeLoanCheck = await _context.LoanHistories.FirstOrDefaultAsync(x=>x.AppUserId == userId);
                if (activeLoanCheck != null && activeLoanCheck.IsActive == true){
                    return BadRequest(new {error_message =$"Active loan of {activeLoanCheck.Amount} already exists."});
                }
                var Loancreate = createLoan.ToCreateLoanDto();
                var createdLoan = await _loanRepo.CreateLoanAsync(Loancreate);
                createdLoan.AppUserId = userId;
                var loanhistory = new LoanHistory{
                    Amount = createdLoan.Amount,
                    AppUserId = userId,
                    IsActive = true,
                    LoanId = createdLoan.Id
                };
                await _context.LoanHistories.AddAsync(loanhistory);
                await _context.SaveChangesAsync();
                return Ok(new {message = $"Loan of {createLoan.Amount} has been successfully disbursed to your account"});
                
            }
            catch(Exception ex){
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get/loanbalance")]
        [Authorize]
        public async Task<IActionResult> GetBalance(){
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var loanBalance =await _loanRepo.GetLoanBalanceAsync(userId);
            if(loanBalance == null){
                return NotFound("You have no active loan");
            }
            return Ok(new {loanBalance = loanBalance.Amount});
        }

        [HttpPost("repayment")]
        [Authorize]
        public async Task<IActionResult> LoanRepayment([FromBody] repaymentDto repayDto){
            try{
                if(!ModelState.IsValid){
                    return BadRequest(ModelState);
                }
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var loan = await _context.Loans.FirstOrDefaultAsync(x=>x.AppUserId == userId && x.Amount > 0);
                var loanhistories = await _context.LoanHistories.FirstOrDefaultAsync(x=>x.AppUserId == userId && x.Amount > 0 && x.IsActive == true);
                if(loan == null){
                    return NotFound("You have not apply for a loan yet");
                }
                
                if(loanhistories != null){
                    if (repayDto.Amount < 0){
                    return BadRequest("Amount must be greater than 0");
                    }
                    if (repayDto.Amount > loan.Amount){
                       return BadRequest($"Amount must be greater than {loan.Amount}"); 
                    }
                    var repayLoan =await _loanRepo.LoanRepaymentAsync(userId, repayDto.Amount);
                    if (repayLoan.Amount < 0){
                        loanhistories.IsActive = false;
                    }
                    return Ok(new {
                        message = $"repayment of {repayDto.Amount} successful, remains {repayLoan.Amount}"
                    });


                }else{
                    return NotFound("You have no applied loan");
                }


            }catch(Exception ex){return StatusCode(500, ex);}
        }


    }
}