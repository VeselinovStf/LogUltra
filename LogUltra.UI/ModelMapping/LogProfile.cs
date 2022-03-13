using AutoMapper;
using LogUltra.Models;
using LogUltra.UI.ViewModels;

namespace LogUltra.UI.ModelMapping
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<Log, LogViewModel>()
                    .ForMember(s => s.CreatedAt, m => m.MapFrom(e => e.CreatedAt.ToString("dd-MM-yyyy hh:mm:ss")));
        }
    }
}
