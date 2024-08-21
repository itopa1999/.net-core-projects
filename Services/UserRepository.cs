using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using onboardingAPI.Data;
using onboardingAPI.Interfaces;
using onboardingAPI.Models;

namespace onboardingAPI.Repository
{
    
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        public UserRepository(
            ApplicationDBContext context,
            UserManager<AppUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;

        }

        public Task<AppUser> CreateAsync(AppUser userModel)
        {
           return null;
        }

       
    }
}