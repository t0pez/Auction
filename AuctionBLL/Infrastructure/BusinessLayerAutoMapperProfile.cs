using AuctionBLL.Dto;
using AuctionDAL.Models;
using AutoMapper;

namespace AuctionBLL.Infrastructure
{
    public class BusinessLayerAutoMapperProfile : Profile
    {
        // TODO: ? IdToEntityConverter
        public BusinessLayerAutoMapperProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();

            CreateMap<LotDto, Lot>()
                .ForMember(lot => lot.HighestPrice,
                    expression => expression.MapFrom(dto => dto.ActualPrice))
                .ForMember(lot => lot.Owner,
                    expression => expression.MapFrom(dto => dto.OwnerId))
                .ForMember(lot => lot.Buyer,
                    expression => expression.MapFrom(dto => dto.BuyerId))
                .ReverseMap();

            CreateMap<WalletDto, Wallet>().ReverseMap();

            CreateMap<Money, MoneyDto>();
            //    .ForAllMembers(expression => expression.MapFrom(money => new MoneyDto(money.Amount, money.Currency)));

            CreateMap<MoneyDto, Money>()
                .ForMember(money => money.Currency,
                    expression => expression.MapFrom(dto => dto.Currency.Value));


        }
    }
}