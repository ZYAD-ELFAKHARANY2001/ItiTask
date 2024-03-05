using AutoMapper;
using Ecommerce.Application.Contract;
using Ecommerce.DTOs.Author;
using Ecommerce.DTOs.ViewResult;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthor _AuthorRepository;
        private readonly IMapper _mapper;
        public AuthorService(IAuthor AuthorRepository)
        {
            _AuthorRepository = AuthorRepository;

        }

        public Task<Author> CreateAsync(Author entity)
        {
            throw new NotImplementedException();
        }

        public Task<Author> DeleteAsync(Author entity)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Author>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ResultDataList<AllAuthorDto>> GetAllAuthors() //10 , 3 -- 20 30
        {
            var AlldAta = (await _AuthorRepository.GetAllAsync());
            var Prds = AlldAta.Select(p => new AllAuthorDto()
            {
                Id = p.Id,
                Name = p.Name,
            }).ToList();
            ResultDataList<AllAuthorDto> resultDataList = new ResultDataList<AllAuthorDto>();
            resultDataList.Entities = Prds;
            resultDataList.Count = AlldAta.Count();
            return resultDataList;
        }

        public Task<Author> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Author> UpdateAsync(Author entity)
        {
            throw new NotImplementedException();
        }
    }
}
