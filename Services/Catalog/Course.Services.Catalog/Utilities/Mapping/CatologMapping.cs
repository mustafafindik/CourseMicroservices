using AutoMapper;
using Course.Services.Catalog.Dtos.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course.Services.Catalog.Entities.Concrete;

namespace Course.Services.Catalog.Utilities.Mapping
{
    public class CatologMapping:Profile
    {
        public CatologMapping()
        {
            CreateMap<Catalog.Entities.Concrete.Course, CourseDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();
        }
    }
}
