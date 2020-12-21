﻿using AutoMapper;
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
            CreateMap<ClientDto, ClientResponseDto>().ReverseMap();
            CreateMap<ClientDto, ClientRequestDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductDto, ProductRequestDto>().ReverseMap();
            CreateMap<ProductDto, ProductResponseDto>().ReverseMap();

            CreateMap<Invoice, InvoiceFullDto>().ReverseMap();
            CreateMap<InvoiceFullDto, InvoiceResponseDto>().ReverseMap();
            CreateMap<InvoiceFullDto, InvoiceResponseDto>().ReverseMap();

        }
        
    }
}
