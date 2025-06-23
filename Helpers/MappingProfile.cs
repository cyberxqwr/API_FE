using AutoMapper;
using Paslauga.Entities;
using Paslauga.Features.vDC;
using Paslauga.Features.Organisation;
using Paslauga.Features.vLAN;
using Paslauga.Features.NetworkPool;
using Paslauga.Features.VM;
using Namotion.Reflection;

namespace Paslauga.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<UpdateVDCRequest, VDC>()
                    .ForAllMembers(o => o.Condition((src, dest, srcMember) =>
                    MappingHelpers.ShouldMap(srcMember)
                    ));

            CreateMap<UpdateOrganisationRequest, Organisation>()
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    MappingHelpers.ShouldMap(srcMember)
                    ));

            CreateMap<UpdateVLANRequest, VLAN>()
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    MappingHelpers.ShouldMap(srcMember)
                    ));
            CreateMap<UpdateNetworkPoolRequest, NetworkPool>()
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    MappingHelpers.ShouldMap(srcMember)
                    ));
            CreateMap<UpdateVMRequest, VM>()
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    MappingHelpers.ShouldMap(srcMember)
                    ));
        }

    }
}
 
