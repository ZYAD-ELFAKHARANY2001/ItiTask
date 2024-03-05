using AutoMapper;
using Ecommerce.DTOs.Author;
using Ecommerce.DTOs.Book;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ecommerce.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUpdateDeleteDto, Book>().ReverseMap();
            CreateMap<GetAllBooksDTO, Book>().ReverseMap();
            CreateMap<AllAuthorDto, Author>().ReverseMap();
        }
    }
}
