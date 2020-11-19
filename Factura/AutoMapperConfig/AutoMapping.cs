using AutoMapper;
using Factura.Dto;
using Factura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura.AutoMapperConfig
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
        }
        
    }
}
