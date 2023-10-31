using AutoMapper;
using InjectionMachineModule.Application.Queries;
using InjectionMachineModule.Application.Queries.Molds;
using InjectionMachineModule.Application.Queries.PlasticInjectionMachines;
using InjectionMachineModule.Dtos.Equipments;

namespace InjectionMachineModule.Mapping;

public class ModelToViewModelProfile : Profile
{
    public ModelToViewModelProfile()
    {
        CreateMap<EquipmentDto, PlasticInjectionMachineViewModel>();

        CreateMap<EquipmentDto, MoldViewModel>()
            .ForMember(dest => dest.MoldId, dest => dest.MapFrom(src => src.EquipmentId));

        CreateMap<PropertyDto, PropertyViewModel>();
    }
}
