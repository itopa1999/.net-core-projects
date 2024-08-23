using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using onboardingAPI.Dtos;
using onboardingAPI.Models;

namespace onboardingAPI.Mappers
{
    public static class BvnMappers
    {
        public static BVNInfo ToCreateBvnDto(this CreateBvnDto createBvnDto){
            return new BVNInfo{
                Gender = createBvnDto.Gender,
                BvnNumber = createBvnDto.BvnNumber
            };
        }

        public static updateBvnDto ToUpdateBvnDto(this updateBvnDto updateBvnDto){
            return new updateBvnDto{
                BvnNumber = updateBvnDto.BvnNumber,
                Gender = updateBvnDto.Gender
            };
        }


    }
}