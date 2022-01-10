using AuctionBLL.Dto;
using AuctionWeb.ViewModels.Lots;
using AutoMapper;
using System;
using AuctionWeb.ViewModels.Money;
using AuctionWeb.ViewModels.News;
using AuctionWeb.ViewModels.Users;

namespace AuctionWeb.Infrastructure
{
    public class PresentationLayerAutoMapperProfile : Profile
    {
        public PresentationLayerAutoMapperProfile()
        {
            CreateMap<LotDto, LotDetailsViewModel>();

            CreateMap<LotDto, LotListViewModel>();
            
            CreateMap<LotCreateViewModel, LotDto>()
                .ForMember(dto => dto.StartDate,
                    expression => expression.MapFrom(model =>
                        new DateTime(model.StartDate.Ticks)
                            .AddTicks(model.StartTime.Ticks)))
                .ForMember(dto => dto.StartPrice,
                    expression => expression.MapFrom(model => 
                        new MoneyDto(model.StartPrice, Convert.ToInt32(model.Currency))))
                .ForMember(dto => dto.MinStepPrice,
                    expression => expression.MapFrom(model => 
                        new MoneyDto(model.MinStepPrice, Convert.ToInt32(model.Currency))));


            CreateMap<LoginInfoModel, UserDto>()
                .ForMember(dto => dto.UserName,
                    expression => expression.MapFrom(model => model.Login));
            
            CreateMap<UserCreateViewModel, UserDto>();
            
            CreateMap<UserDto, UserListModel>();
            
            CreateMap<UserDto, UserDetailsViewModel>();

            
            CreateMap<MoneyCreateViewModel, MoneyDto>();


            CreateMap<NewsDto, NewsListViewModel>().ReverseMap();
            
            CreateMap<NewsDto, NewsEditViewModel>().ReverseMap();
            
            CreateMap<NewsCreateViewModel, NewsDto>();
        }
    }
}