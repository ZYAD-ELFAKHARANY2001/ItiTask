using Ecommerce.Application.Contract;
using Ecommerce.Context;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure
{
    public class AuthorRepsitory : Repository<Author, int>, IAuthor
    {
        public AuthorRepsitory(EcommerceDbcontext ecommerceContext) : base(ecommerceContext)
        {

        }
    }
}
