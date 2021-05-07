using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Dtos.Concrete
{
    public class CourseDto
    {    
        public string Id { get; set; }
        public string CategoryId { get; set; }
        public CategoryDto Category { get; set; }
       
        public string CourseName { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserId { get; set; }
        public FeatureDto Feature { get; set; } 
        public List<string> Languages { get; set; } 
    }
}
