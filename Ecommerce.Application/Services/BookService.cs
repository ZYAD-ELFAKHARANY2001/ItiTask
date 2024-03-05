using AutoMapper;
using Ecommerce.Application.Contract;
using Ecommerce.DTOs.Author;
using Ecommerce.DTOs.Book;
using Ecommerce.DTOs.ViewResult;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class BookService:IBookService
    {
        private readonly IBook _bookRepository;
        private readonly IMapper _mapper;
        public BookService(IBook bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<ResultView<CreateUpdateDeleteDto>> Create(CreateUpdateDeleteDto book)
        {
            var Query = (await _bookRepository.GetAllAsync()); // se;ect * from product
            var OldBook = Query.Where(b => b.Name == book.Name).FirstOrDefault();
            if (OldBook != null)
            {
                return new ResultView<CreateUpdateDeleteDto> { Entity = null, IsSuccess = false, Message = "Already Exist" };
            }
            else
            {
                var Prd = _mapper.Map<Book>(book);
                var NewPrd = await _bookRepository.CreateAsync(Prd);
                await _bookRepository.SaveChangesAsync();
                var PrdDto = _mapper.Map<CreateUpdateDeleteDto>(NewPrd);
                return new ResultView<CreateUpdateDeleteDto> { Entity = PrdDto, IsSuccess = true, Message = "Created Successfully" };
            }

        }

        public async Task<ResultView<CreateUpdateDeleteDto>> HardDelete(CreateUpdateDeleteDto book)
        {
            try
            {
                var PRd = _mapper.Map<Book>(book);
                var Oldprd = _bookRepository.DeleteAsync(PRd);
                await _bookRepository.SaveChangesAsync();
                var PrdDto = _mapper.Map<CreateUpdateDeleteDto>(Oldprd);
                return new ResultView<CreateUpdateDeleteDto> { Entity = PrdDto, IsSuccess = true, Message = "Deleted Successfully" };
            }
            catch (Exception ex)
            {
                return new ResultView<CreateUpdateDeleteDto> { Entity = null, IsSuccess = false, Message = ex.Message };

            }
        }
        public async Task<ResultView<CreateUpdateDeleteDto>> SoftDelete(CreateUpdateDeleteDto book)
        {
            try
            {
                var PRd = _mapper.Map<Book>(book);
                var Oldprd = (await _bookRepository.GetAllAsync()).FirstOrDefault(p => p.Id == book.Id);
                Oldprd.IsDeleted = true;
                await _bookRepository.SaveChangesAsync();
                var PrdDto = _mapper.Map<CreateUpdateDeleteDto>(Oldprd);
                return new ResultView<CreateUpdateDeleteDto> { Entity = PrdDto, IsSuccess = true, Message = "Deleted Successfully" };
            }
            catch (Exception ex)
            {
                return new ResultView<CreateUpdateDeleteDto> { Entity = null, IsSuccess = false, Message = ex.Message };

            }
        }

        public async Task<ResultDataList<GetAllBooksDTO>> GetAllPagination(int items, int pagenumber) //10 , 3 -- 20 30
        {
            var AlldAta = (await _bookRepository.GetAllAsync());
            var Prds = AlldAta.Skip(items * (pagenumber - 1)).Take(items)
                                              .Select(p => new GetAllBooksDTO()
                                              {
                                                  Id = p.Id,
                                                  Name = p.Name,
                                                  Price = (decimal)p.Price,
                                                  AuthorName = p.Author.Name
                                              }).ToList();
            ResultDataList<GetAllBooksDTO> resultDataList = new ResultDataList<GetAllBooksDTO>();
            resultDataList.Entities = Prds;
            resultDataList.Count = AlldAta.Count();
            return resultDataList;
        }

        public async Task<CreateUpdateDeleteDto> GetOne(int ID)
        {
            var prd = await _bookRepository.GetByIdAsync(ID);
            var REturnPrd = _mapper.Map<CreateUpdateDeleteDto>(prd);
            return REturnPrd;
        }

        public Task<Book> CreateAsync(Book entity)
        {
            throw new NotImplementedException();
        }

      
        public Task<Book> DeleteAsync(Book entity)
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Book>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        //public Task<CreateUpdateDeleteDto> UpdateAsync(CreateUpdateDeleteDto book)
        //{
        //    try
        //    {
        //        var book = await _mapper.Map<Book>(entity);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        public Task<Book> UpdateAsync(Book entity)
        {
            throw new NotImplementedException();
        }


        async Task<ResultView<CreateUpdateDeleteDto>> IBookService.UpdateAsync(CreateUpdateDeleteDto book)
        {
            var res = _mapper.Map<Book>(book);
            var prd =  await _bookRepository.UpdateAsync(res);
            CreateUpdateDeleteDto REturnPrd = _mapper.Map<CreateUpdateDeleteDto>(prd);
            ResultView<CreateUpdateDeleteDto> resultView = new ResultView<CreateUpdateDeleteDto>();
            resultView.IsSuccess = true;
            resultView.Entity = REturnPrd;
            await _bookRepository.SaveChangesAsync();
            return resultView;
        }
    }
}
