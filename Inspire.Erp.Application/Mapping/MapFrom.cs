using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Mapping
{
    public class MapFrom<T> : Profile, IMapFrom<T> 
    {
    public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }
    }
}
