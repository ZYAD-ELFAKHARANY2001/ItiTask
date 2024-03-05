using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Contract
{
    public interface IAuthor:IRepository<Author,int>
    {
    }
}
