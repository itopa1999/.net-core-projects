using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace onboardingAPI.Dtos
{
    public class BvnDto
    {
        public int BvnNumber { get; set; }
        public DateOnly DOB { get; set; } 
        public string? Gender { get; set; }
        public bool Verified { get; set; } = false;
    }
    public class CreateBvnDto{
        [Required]
        public int BvnNumber { get; set; }
        [Required]
        public string? Gender { get; set; }
    }

    public class updateBvnDto{
        [Required]
        public int BvnNumber { get; set; }
        [Required]
        public string? Gender { get; set; }
    }
    
    
}