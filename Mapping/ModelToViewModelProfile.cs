using AutoMapper;
using InjectionMachineModule.Application.Queries;
using InjectionMachineModule.Application.Queries.ManufacturingOrders;
using InjectionMachineModule.Application.Queries.Molds;
using InjectionMachineModule.Application.Queries.PlasticInjectionMachines;
using InjectionMachineModule.Application.Queries.PlasticMaterials;
using InjectionMachineModule.Application.Queries.PlasticProducts;

namespace InjectionMachineModule.Mapping;

public class ModelToViewModelProfile : Profile
{
    public ModelToViewModelProfile()
    {
        CreateMap<EquipmentViewModelDto, PlasticInjectionMachineViewModel>();
        CreateMap<EquipmentViewModelDto, MoldViewModel>()
            .ForMember(dest => dest.MoldId, dest => dest.MapFrom(src => src.EquipmentId));

        CreateMap<MaterialDefinitionViewModelDto, PlasticProductViewModel>()
            .ForMember(dest => dest.PlasticProductId, dest => dest.MapFrom(src => src.MaterialDefinitionId));
        CreateMap<MaterialDefinitionViewModelDto, PlasticMaterialViewModel>()
            .ForMember(dest => dest.PlasticMaterialId, dest => dest.MapFrom(src => src.MaterialDefinitionId));
        CreateMap<OperationViewModelDto, OperationViewModel>();
        CreateMap<MaterialUnitViewModelDto, MaterialUnitViewModel>();

        CreateMap<ManufacturingOrderDto, ManufacturingOrderViewModel>();

        CreateMap<PropertyDto, PropertyViewModel>();

    }
}
