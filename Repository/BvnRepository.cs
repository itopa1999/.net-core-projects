using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using onboardingAPI.Data;
using onboardingAPI.Dtos;
using onboardingAPI.Interfaces;
using onboardingAPI.Models;

namespace onboardingAPI.Repository
{
    public class BvnRepository : IBvnRepository
    {
        private readonly ApplicationDBContext _context;
        public BvnRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<BVNInfo> CreateBvnAsync(BVNInfo bvnModel)
        {
            await _context.BVNInfos.AddAsync(bvnModel);
            await _context.SaveChangesAsync();
            return bvnModel;
            
        }

        public async Task<BVNInfo?> GetBVNInfoAsync(string userId)
        {
            var bvninfo = await _context.BVNInfos.FirstOrDefaultAsync(x=>x.AppUserId == userId);
            if (bvninfo == null){
                return null;
            }
            return bvninfo;
        }

        public async Task<BVNInfo?> UpdateBvnAsync(int id, updateBvnDto updateBvnDto)
        {
            var bvninfo = await _context.BVNInfos.FirstOrDefaultAsync(x=>x.Id == id);
            if(bvninfo == null){
                return null;
            }
            bvninfo.Gender = updateBvnDto.Gender;
            bvninfo.BvnNumber = updateBvnDto.BvnNumber;
            await _context.SaveChangesAsync();
            return bvninfo;
        }
    }
}