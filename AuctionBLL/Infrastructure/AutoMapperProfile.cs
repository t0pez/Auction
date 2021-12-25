using AuctionBLL.Dto;
using AuctionDAL.Models;
using AutoMapper;

namespace AuctionBLL.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        // TODO: ? IdToEntityConverter
        public AutoMapperProfile()
        {
            CreateMap<UserDto, User>()
                .ForMember(user => user.Id,
                    expression => expression.MapFrom(dto => dto.Id))
                .ForMember(user => user.UserName,
                    expression => expression.MapFrom(dto => dto.UserName))
                .ForMember(user => user.FirstName,
                    expression => expression.MapFrom(dto => dto.FirstName))
                .ForMember(user => user.LastName,
                    expression => expression.MapFrom(dto => dto.LastName))
                .ForMember(user => user.Wallet,
                    expression => expression.MapFrom(dto => dto.Wallet))
                .ForMember(user => user.OwnedLots,
                    expression => expression.MapFrom(dto => dto.OwnedLots))
                .ForMember(user => user.LotsAsParticipant,
                    expression => expression.MapFrom(dto => dto.AsParticipant)).ReverseMap();

            CreateMap<LotDto, Lot>()
                .ForMember(lot => lot.Id,
                    expression => expression.MapFrom(dto => dto.Id))
                .ForMember(lot => lot.Name,
                    expression => expression.MapFrom(dto => dto.Name))
                .ForMember(lot => lot.Description,
                    expression => expression.MapFrom(dto => dto.Description))
                .ForMember(lot => lot.StartPrice,
                    expression => expression.MapFrom(dto => dto.StartPrice))
                .ForMember(lot => lot.HighestPrice,
                    expression => expression.MapFrom(dto => dto.ActualPrice))
                .ForMember(lot => lot.MinStepPrice,
                    expression => expression.MapFrom(dto => dto.MinStepPrice))
                .ForMember(lot => lot.DateOfCreation,
                    expression => expression.MapFrom(dto => dto.DateOfCreation))
                .ForMember(lot => lot.StartDate,
                    expression => expression.MapFrom(dto => dto.StartDate))
                .ForMember(lot => lot.EndDate,
                    expression => expression.MapFrom(dto => dto.EndDate))
                .ForMember(lot => lot.ProlongationTime,
                    expression => expression.MapFrom(dto => dto.ProlongationTime))
                .ForMember(lot => lot.TimeForStep,
                    expression => expression.MapFrom(dto => dto.TimeForStep))
                .ForMember(lot => lot.Owner,
                    expression => expression.MapFrom(dto => dto.OwnerId))
                .ForMember(lot => lot.Buyer,
                    expression => expression.MapFrom(dto => dto.BuyerId))
                .ForMember(lot => lot.Participants,
                    expression => expression.MapFrom(dto => dto.Participants))
                .ForMember(lot => lot.Steps,
                    expression => expression.MapFrom(dto => dto.Steps)).ReverseMap();
        }
    }
}