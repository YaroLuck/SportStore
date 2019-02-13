using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportStore.Domain.Entities;

namespace SportStore.Domain.Abstract
{
    // позволяет получить последовательность обьектов Product и не требует указаний на то как и где хранятся или как следует их ивлекать
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}
