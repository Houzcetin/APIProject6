using APIProject6.WebAPI.Dtos.FeatureDtos;
using APIProject6.WebAPI.Dtos.MessageDtos;
using APIProject6.WebAPI.Entities;
using AutoMapper;

namespace APIProject6.WebAPI.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap < Feature, ResultFeatureDto>().ReverseMap();
            CreateMap < Feature, CreateFeatureDto>().ReverseMap();
            CreateMap < Feature, GetByIdFeatureDto>().ReverseMap();
            CreateMap < Feature, UpdateFeatureDto>().ReverseMap();

            CreateMap<Message, ResultMessageDto>().ReverseMap();
            CreateMap<Message, CreateMessageDto>().ReverseMap();
            CreateMap<Message, UpdateMessageDto>().ReverseMap();
            CreateMap<Message, GetByIdMessageDto>().ReverseMap();

        }
    }
}
