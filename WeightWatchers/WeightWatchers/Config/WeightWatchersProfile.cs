using AutoMapper;
using Subscriber.Core.DTO;
using Subscriber.Core.Model;
using Subscriber.Core.Response;
using Subscriber.Data.Entities;
using System.Security.Claims;

namespace Subscriber.WebApi.Config
{
    public class WeightWatchersProfile : Profile
    {
        public WeightWatchersProfile()
        {
            //CreateMap<CardDTO, Card>().ReverseMap();
            CreateMap<BaseResponseGeneral<Card>, CardDTO>()
    .ForMember(dest => dest.UpDate, opt => opt.MapFrom(src => src.Response.UpDate))
    .ForMember(dest => dest.BMI, opt => opt.MapFrom(src => src.Response.BMI))
    .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Response.Height))
    .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Response.Weight)).ReverseMap();

            CreateMap<SubscriberModle, SubscriberDTO>().ForMember(dest => dest.Height, opt => opt.Ignore()).ReverseMap();//ברצוני שיהיה מבלי הגובה
            CreateMap<SubscriberModle, Subscribers>().ReverseMap();

        }
    }
}
