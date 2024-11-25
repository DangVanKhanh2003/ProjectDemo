using AutoMapper;
using ProjectDemo.Entites;
using ProjectDemo.Model;
using ProjectDemo.ViewModel;

namespace ProjectDemo.Helper
{
    public class AutoMapperConfiguration: Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Categories, CategoryModel>().ReverseMap();
            CreateMap<Categories, CategoryVM>().ReverseMap();
            CreateMap<Products, ProductModel>().ReverseMap();
            CreateMap<Products, ProductVM>()
                 .ForMember(dest => dest.category, opt => opt.MapFrom(src => src.Categories))
                .ReverseMap();

        }
    }
}
