using AutoMapper;
using InjectionMachineModule.Application.Dtos;
using InjectionMachineModule.Application.Dtos.Equipments;
using InjectionMachineModule.Application.Queries;
using InjectionMachineModule.Application.Queries.Molds;
using InjectionMachineModule.Application.Queries.PlasticInjectionMachines;

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
