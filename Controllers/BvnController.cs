using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using onboardingAPI.Data;
using onboardingAPI.Dtos;
using onboardingAPI.Interfaces;
using onboardingAPI.Mappers;

namespace onboardingAPI.Controllers
{
    [Route("api/bvn")]
    [ApiController]
    public class BvnController : ControllerBase
    {
        private readonly IBvnRepository _bvnRepo;
        private readonly ApplicationDBContext _context;
        public BvnController(IBvnRepository bvnRepo, ApplicationDBContext context)
        {
            _bvnRepo = bvnRepo;
            _context = context;
        }

        [HttpPost("create/bvninfo")]
        [Authorize]
        public async Task<IActionResult> CreateBvn([FromBody] CreateBvnDto createBvnDto){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var bvnModel = createBvnDto.ToCreateBvnDto();
            var bvncreated = await _bvnRepo.CreateBvnAsync(bvnModel);
                bvncreated.AppUserId = userId;
                bvncreated.Verified = true;
                await _context.SaveChangesAsync();
            return Ok("created successfully");

        }

        [HttpGet("get/bvninfo")]
        [Authorize]
        public async Task<IActionResult> GetBvnInfo(){
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var fetchbvn = await _bvnRepo.GetBVNInfoAsync(userId);
            if (fetchbvn == null){
                return NotFound(new{Error_Message="BVN info not found"});
            }
            return Ok(fetchbvn);
        }

        [HttpPost("{id:int}/update/bvninfo")]
        [Authorize]
        public async Task<IActionResult> updateBvnInfo([FromBody] updateBvnDto updateBvnDto, [FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var updatedbvn = await _bvnRepo.UpdateBvnAsync(id, updateBvnDto);
            if (updatedbvn == null){
                return NotFound(new{Error_Message="BVN info to update not found"});
            }
            return Ok(new{Message="BVN info updated successfully"});
        }

        [HttpGet("demo")]
        public IActionResult RedirectToGoogle()
        {
            return Redirect("https://www.google.com"); // Returns 302 Found and redirects to Google
        }

        
    }
}