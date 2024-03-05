using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.Book
{
    public class CardDto:GetAllBooksDTO
    {

        public int Quantity { get; set; }
        public string? Description { get; set; }
    }
}
