using AutoMapper;
using Project.MVC.Models;
using Project.Service.Models;

namespace Project.MVC
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VehicleMake, VehicleMakeVM>();
            CreateMap<VehicleMakeVM, VehicleMake>();
            CreateMap<VehicleModel, VehicleModelVM>();
            CreateMap<VehicleModelVM, VehicleModel>();
        }
    }
}
