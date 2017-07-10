using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClimateStore.Domain.Abstract;
using ClimateStore.Domain.Entities;

namespace ClimateStore.Domain.Concrete
{
   public class EFProductRepository: IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Product> Products
        {
            get { return context.Products; }
        }
    }
}
/*
 класс хранилища реализует интерфейс IProductRepository и использует экземпляр EFDbContext, чтобы извлекать данные из базы с помощью Entity Framework
*/
