using Ecommerce.Application.Contract;
using Ecommerce.DTOs.Author;
using Ecommerce.DTOs.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public interface IAuthorService:IAuthor
    {
        Task<ResultDataList<AllAuthorDto>> GetAllAuthors();
    }
}
