using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using onboardingAPI.Data;
using onboardingAPI.Dtos;
using onboardingAPI.Interfaces;
// using onboardingAPI.Mappers;
using onboardingAPI.Models;
using onboardingAPI.Repository;
using onboardingAPI.Services;

namespace onboardingAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtRepository _token;
        private readonly ApplicationDBContext _context;
        public UserController(
            IUserRepository userRepo, 
            UserManager<AppUser> userManager,
            IJwtRepository token,
            ApplicationDBContext context,
            SignInManager<AppUser> signInManager
            )
        {
            _userRepo = userRepo;
            _userManager = userManager;
            _token = token;
            _context = context;
            _signInManager = signInManager;
        }

        [HttpPost("create/user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            try{
                var user = new AppUser{
                    UserName = createUserDto.Username,
                    Email = createUserDto.Email,
                    FirstName = createUserDto.FirstName,
                    LastName = createUserDto.LastName,
                    HouseNo = createUserDto.HouseNo,
                    StreetName = createUserDto.StreetName,
                    State = createUserDto.State,
                    LGA = createUserDto.LGA,
                    AccountType = createUserDto.AccountType,
                    };
                var userModel = await _userManager.CreateAsync(user, createUserDto.Password);
                if (userModel.Succeeded){
                    var role = await _userManager.AddToRoleAsync(user, "User");
                    if (role.Succeeded){
                        var otp = new Otp{
                            AppUserId = user.Id,
                            Token = _token.GenerateToken(),

                        };
                        await _context.Otps.AddAsync(otp);
                        await _context.SaveChangesAsync();
                        Console.WriteLine($"your verification code is {otp.Token}");
                        return Ok(new{
                            message ="Account Successfully Created, please verify your account",
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                        });
                    }return StatusCode(500, role.Errors);
                }return StatusCode(500, userModel.Errors);
            }catch(Exception e){
                return StatusCode(500, e);
            };
        }

        [HttpPost("token/verification")]
        public async Task<IActionResult> VerifyToken([FromBody] VerifyTokenDto verifyTokenDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var token = await _context.Otps.FirstOrDefaultAsync(x=>x.Token == verifyTokenDto.Token);
            if (token == null){
                return NotFound(new {message = "Token is incorrect"});
            }
            if (token.IsActive == false){
                return BadRequest(new {message = "Token has expired"});
            }
            if (token.CreatedAt.AddMinutes(10) <= DateTime.Now ){
                token.IsActive = false;
                await _context.SaveChangesAsync();
                return BadRequest(new {message = "token has expired"});
            }else{
                var user = await _userManager.FindByIdAsync(token.AppUserId);
                user.Verified = true;
                user.VerifiedAt = DateTime.Now;
                user.IsActive =true;
                await _context.SaveChangesAsync();
                token.IsActive = false;
                await _context.SaveChangesAsync();

                return Ok(new {message="verified successfully"});
            }
        }

        [HttpPost("resend/token")]
        public async Task<IActionResult> ResendToken([FromBody] ResendTokenDto resendTokenDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByEmailAsync(resendTokenDto.Email);
            if (user == null){
                return BadRequest("Email is not attached to any user");
            }
            if (user.IsActive == true){
                return BadRequest("Account has been verified already");
            }
            var getOtp = await _context.Otps.FirstOrDefaultAsync(x=>x.AppUserId == user.Id);
            getOtp.IsActive = true;
            getOtp.CreatedAt = DateTime.Now;
            getOtp.Token = _token.GenerateToken();
            await _context.SaveChangesAsync();
            Console.WriteLine($"your verification code is {getOtp.Token}");
            return Ok("token resend");
        }

        [HttpPost("bvn/verification")]
        public async Task<IActionResult> BvnVerification([FromBody] BvnVerificationDto bvnDto){
            try{
                if(!ModelState.IsValid){
                    return BadRequest(ModelState);
                }
                var user = await _userManager.FindByEmailAsync(bvnDto.Email);
                if (user == null){
                    return BadRequest("Email is not attached to any user");
                }
                if (user.Verified == false){
                    return BadRequest("Please verify your account before proceeding");
                }
                var bvnModel = new BVNInfo{
                    AppUserId = user.Id,
                    BvnNumber = bvnDto.BvnNumber,
                    Gender = bvnDto.Gender,
                    // DOB = DateOnly,
                    Verified = true
                };
                await _context.BVNInfos.AddAsync(bvnModel);
                await _context.SaveChangesAsync();
                return Ok("BVN verified successfully");
            }catch(Exception e){return StatusCode(500, "Already created");}
        }
        
        [HttpPost("create/businessInfo")]
        public async Task<IActionResult> CreateBusinessInfo([FromBody] BusinessInfoDto businessInfoDto){
            try{
                if(!ModelState.IsValid){
                    return BadRequest(ModelState);
                }
                var user = await _userManager.FindByEmailAsync(businessInfoDto.Email);
                if (user == null){
                    return BadRequest("Email is not attached to any user");
                }
                if (user.Verified == false){
                    return BadRequest("Please verify your account before proceeding");
                }
                var businessInfo = new BusinessInfo{
                    AppUserId = user.Id,
                    BusinessName = businessInfoDto.BusinessName,
                    BusinessCategory= businessInfoDto.BusinessCategory,
                    HouseNo= businessInfoDto.HouseNo,
                    StreetName= businessInfoDto.StreetName,
                    State= businessInfoDto.State,
                    LGA= businessInfoDto.LGA,
                    BusinessType= businessInfoDto.BusinessType,
                    RegNumber= businessInfoDto.RegNumber,
                    Verified = true
                };
                await _context.BusinessInfos.AddAsync(businessInfo);
                await _context.SaveChangesAsync();
                return Ok("Business Info has been verified successfully");
            }catch(Exception e){return StatusCode(500, "Already created");}
        }

        [HttpPost("create/IdentityInfo")]
        public async Task<IActionResult> CreateBusinessInfo([FromBody] IdentityInfoDto identityInfoDto){
        try{
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByEmailAsync(identityInfoDto.Email);
            if (user == null){
                return BadRequest("Email is not attached to any user");
            }
            var identityInfo = new IDInfo{
                AppUserId = user.Id,
                IdentityNumber = identityInfoDto.IdentityNumber,
                IdentityType= identityInfoDto.IdentityType,
                Verified = true
            };
            await _context.IDInfos.AddAsync(identityInfo);
            await _context.SaveChangesAsync();

            user.AccountNumber = _token.GenerateAccountNumber();
            await _context.SaveChangesAsync();
            return Ok("Identity Info has been verified successfully");
        }
        catch(Exception e){return StatusCode(500, "Already created");}

        }

        [HttpPost("user/login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginDto loginDto){
            var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.UserName == loginDto.Username);
            if(user == null){
                return BadRequest("incorrect credentials");
            }
            var userLogin = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!userLogin.Succeeded){
                return BadRequest("invalid credentials");
            }
            return Ok(new {
                message = "login successfully",
                account = user.AccountNumber,
                firstname = user.FirstName,
                lastname = user.LastName,
                email = user.Email,
                username = user.UserName,
                token = _token.CreateToken(user),
            });
        }
    }
}