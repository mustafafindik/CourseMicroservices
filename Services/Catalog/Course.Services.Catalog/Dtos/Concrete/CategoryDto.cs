using Course.Services.Catalog.Dtos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Dtos.Concrete
{
    public class CategoryDto :IDto
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
    }
}
