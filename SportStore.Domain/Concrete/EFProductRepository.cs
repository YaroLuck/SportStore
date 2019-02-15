using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;

namespace SportStore.Domain.Concrete
{
    //класс хранилища. Реализует интерфейс и использует экземпляр контекста чтобы извлекать данные из базы с помощью ЕФ.
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Product> Products
        {
            get { return context.Products; }
        }

    }
}
