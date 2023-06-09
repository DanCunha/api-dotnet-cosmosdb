using AutoMapper;
using ChallengeAPI.Models.Dtos;
using ChallengeAPI.Models.Entities;

namespace ChallengeAPI.Mappers
{
    public class ChallengeProfile : Profile
    {
        public ChallengeProfile()
        {
            CreateMap<Product, ProductDto>();            
        }
    }
}
