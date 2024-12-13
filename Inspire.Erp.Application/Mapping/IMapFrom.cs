using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Mapping
{
    public interface IMapFrom<T>
    {
        public void  Mapping(Profile profile);
    }
}
