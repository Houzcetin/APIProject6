using APIProject6.WebAPI.Dtos.AboutDtos;
using APIProject6.WebAPI.Dtos.CategoryDtos;
using APIProject6.WebAPI.Dtos.FeatureDtos;
using APIProject6.WebAPI.Dtos.MessageDtos;
using APIProject6.WebAPI.Dtos.NotificationDtos;
using APIProject6.WebAPI.Dtos.ProductDtos;
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

            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, ResultProductWithCategoryDto>().ForMember(x => x.CategoryName, y => y.MapFrom
            (z => z.Category.CategoryName)).ReverseMap();

            CreateMap<Notification,ResultNotificationDto>().ReverseMap();
            CreateMap<Notification, CreateNotificationDto>().ReverseMap();
            CreateMap<Notification, GetNotificationByIdDto>().ReverseMap();
            CreateMap<Notification, UpdateNotificationDto>().ReverseMap();

            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();

            CreateMap<About, ResultAboutDto>().ReverseMap();
            CreateMap<About, CreateAboutDto>().ReverseMap();
            CreateMap<About, UpdateAboutDto>().ReverseMap();
            CreateMap<About, GetAboutByIdDto>().ReverseMap();
        }
    }
}
