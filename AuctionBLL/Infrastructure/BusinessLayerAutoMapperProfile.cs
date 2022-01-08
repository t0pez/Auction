using System;
using System.Linq;
using AuctionBLL.Dto;
using AuctionBLL.Enums;
using AuctionDAL.Models;
using AutoMapper;

namespace AuctionBLL.Infrastructure
{
    public class BusinessLayerAutoMapperProfile : Profile
    {
        public BusinessLayerAutoMapperProfile()
        {
            CreateMap<Currency, int>().ConvertUsing(currency => currency.Value);
            CreateMap<int, Currency>().ConvertUsing(i => Currency.FromValue(i));

            CreateMap<Money, MoneyDto>();

            CreateMap<MoneyDto, Money>()
                .ForMember(money => money.Currency,
                    expression => expression.MapFrom(dto => dto.Currency));


            CreateMap<WalletDto, Wallet>().ReverseMap();


            CreateMap<UserDto, User>().ReverseMap();

            CreateMap<LotDto, Lot>()
                .ForMember(lot => lot.HighestPrice,
                    expression => expression.MapFrom(dto => dto.ActualPrice))
                .ForMember(lot => lot.Owner,
                    expression => expression.MapFrom(dto => dto.Owner))
                .ForMember(lot => lot.Acquirer,
                    expression => expression.MapFrom(dto => dto.Acquirer))
                .ForMember(lot => lot.Status,
                    expression => expression.MapFrom(dto => (int) dto.Status));
            
            CreateMap<Lot, LotDto>()
                .ForMember(dto => dto.ActualPrice,
                    expression => expression.MapFrom(lot => lot.HighestPrice))
                .ForMember(dto => dto.Owner,
                    expression => expression.MapFrom(lot => lot.Owner))
                .ForMember(dto => dto.Acquirer,
                    expression => expression.MapFrom(lot => lot.Acquirer))
                .ForMember(dto => dto.Status,
                    expression => expression.MapFrom(lot => (LotStatus) lot.Status));

            

        }
    }
}