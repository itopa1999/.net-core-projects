using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onboardingAPI.Models
{
    public class UserActions
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? DeviceInfo { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? Status { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

    }
}