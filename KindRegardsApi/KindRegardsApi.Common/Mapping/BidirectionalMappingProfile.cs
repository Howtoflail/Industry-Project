using AutoMapper;

namespace KindRegardsApi.Common.Mapping
{
    public class BidirectionalMappingProfile<TSource, TDestination> : Profile
    {
        public BidirectionalMappingProfile()
        {
            CreateMap<TSource, TDestination>().ReverseMap();
        }
    }
}
