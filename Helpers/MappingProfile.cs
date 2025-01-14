using AutoMapper;
using Paslauga.Entities;
using Paslauga.Features.vDC;
using Paslauga.Features.Organisation;
using Paslauga.Features.vLAN;
using Paslauga.Features.NetworkPool;

namespace Paslauga.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<UpdateVDCRequest, VDC>()
                    .ForAllMembers(o => o.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateOrganisationRequest, Organisation>()
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateVLANRequest, VLAN>()
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UpdateNetworkPoolRequest, NetworkPool>()
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
