using AuctionBLL.Dto;
using AuctionWeb.ViewModels.Lots;
using AutoMapper;
using System;
using AuctionWeb.ViewModels.Users;

namespace AuctionWeb.Infrastructure
{
    public class PresentationLayerAutoMapperProfile : Profile
    {
        public PresentationLayerAutoMapperProfile()
        {
            CreateMap<LotDto, DetailsModel>();
            
            CreateMap<CreateModel, LotDto>()
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

            CreateMap<LotDto, ListModel>();

            CreateMap<UserDto, UserListModel>();
        }
    }
}