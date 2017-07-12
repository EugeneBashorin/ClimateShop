using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClimateStore.Domain.Entities;

namespace ClimateStore.WebUI.Models
{
    //экземпляр класса модели представления PagingInfo в представление
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}