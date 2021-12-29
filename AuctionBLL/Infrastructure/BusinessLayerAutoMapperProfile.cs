using System;
using AuctionBLL.Dto;
using AuctionBLL.Enums;
using AuctionDAL.Models;
using AutoMapper;

namespace AuctionBLL.Infrastructure
{
    public class BusinessLayerAutoMapperProfile : Profile
    {
        // TODO: ? IdToEntityConverter
        public BusinessLayerAutoMapperProfile()
        {
            CreateMap<Money, MoneyDto>().ConstructUsing(money => new MoneyDto(money.Amount, money.Currency));

            CreateMap<MoneyDto, Money>()
                .ForMember(money => money.Currency,
                    expression => expression.MapFrom(dto => dto.Currency.Value));


            CreateMap<WalletDto, Wallet>().ReverseMap();


            CreateMap<UserDto, User>().ReverseMap();

            CreateMap<LotDto, Lot>()
                .ForMember(lot => lot.HighestPrice,
                    expression => expression.MapFrom(dto => dto.ActualPrice))
                .ForMember(lot => lot.Owner,
                    expression => expression.MapFrom(dto => dto.Owner))
                .ForMember(lot => lot.Buyer,
                    expression => expression.MapFrom(dto => dto.Buyer))
                .ForMember(lot => lot.Status,
                    expression => expression.MapFrom(dto => (int) dto.Status));
            
            CreateMap<Lot, LotDto>()
                .ForMember(dto => dto.ActualPrice,
                    expression => expression.MapFrom(lot => lot.HighestPrice))
                .ForMember(dto => dto.Owner,
                    expression => expression.MapFrom(lot => lot.Owner))
                .ForMember(dto => dto.Buyer,
                    expression => expression.MapFrom(lot => lot.Buyer))
                .ForMember(dto => dto.Status,
                    expression => expression.MapFrom(lot => (LotStatus) lot.Status));

            

        }
    }
}