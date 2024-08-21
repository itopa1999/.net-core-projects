using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using onboardingAPI.Models;

namespace onboardingAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> CreateAsync(AppUser userModel);
        
    }
}