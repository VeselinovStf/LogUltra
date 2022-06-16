using AutoMapper;
using LogUltra.Log.API.DTOs;
using LogUltra.Log.Service.DTOs;

namespace LogUltra.Log.API.ModelMaping
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<LogDTO, LogResponseDTO>()
                   .ForMember(s => s.CreatedAt, m => m.MapFrom(e => e.CreatedAt.ToString("dd-MM-yyyy hh:mm:ss")));
        }
    }
}
