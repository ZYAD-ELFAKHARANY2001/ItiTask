using Ecommerce.Application.Contract;
using Ecommerce.DTOs.Author;
using Ecommerce.DTOs.Book;
using Ecommerce.DTOs.ViewResult;

namespace Ecommerce.Application.Services
{
    public interface IBookService:IBook
    {
        Task<ResultView<CreateUpdateDeleteDto>> Create(CreateUpdateDeleteDto product);
        Task<ResultView<CreateUpdateDeleteDto>> HardDelete(CreateUpdateDeleteDto product);
        Task<ResultView<CreateUpdateDeleteDto>> SoftDelete(CreateUpdateDeleteDto product);
        Task<ResultDataList<GetAllBooksDTO>> GetAllPagination(int items, int pagenumber);
        Task<CreateUpdateDeleteDto> GetOne(int ID);
        Task<ResultView<CreateUpdateDeleteDto>> UpdateAsync(CreateUpdateDeleteDto book);
    }
}