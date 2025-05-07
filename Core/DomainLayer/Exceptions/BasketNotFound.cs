using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BasketNotFound(string id) : NotFoundException($"Basket With Id {id} is Not Found !!")
    {
    }
}
