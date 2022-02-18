using AutoMapper;
using Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Utility._AutoMapper
{
    public class CustomAutoMapperProfile:Profile
    {
        public CustomAutoMapperProfile()
        {
            base.CreateMap<WriterInfo, WriterDTO>();
            base.CreateMap<BlogNews, BlogNewsDTO>()
             .ForMember(x => x.TypeName, source => source.MapFrom(src => src.TypeInfo.Name))
             .ForMember(x => x.WriterName, source => source.MapFrom(src => src.WriterInfo.Name));
        }
    }
}
