using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using onboardingAPI.Dtos;
using onboardingAPI.Models;

namespace onboardingAPI.Interfaces
{
    public interface IBvnRepository
    {
        Task<BVNInfo> CreateBvnAsync(BVNInfo bvnModel);
        Task<BVNInfo?> UpdateBvnAsync(int id, updateBvnDto updateBvnDto);
        Task<BVNInfo?> GetBVNInfoAsync(string userId);
    }
}