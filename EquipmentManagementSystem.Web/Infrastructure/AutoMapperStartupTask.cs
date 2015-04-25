using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using EquipmentManagementSystem.Core.Domain.Catalog;
using EquipmentManagementSystem.Core.Infrastructure;
using EquipmentManagementSystem.Web.Models.Catalog;

namespace EquipmentManagementSystem.Web.Infrastructure
{
     public class AutoMapperStartupTask : IStartupTask
    
    {
         public void Execute()
         {
             //category
             Mapper.CreateMap<Category, CategoryModel>()
                 .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
             Mapper.CreateMap<CategoryModel, Category>()
                 .ForMember(dest => dest.Deleted, mo => mo.Ignore());


             //product
             Mapper.CreateMap<Product, ProductModel>()
                 .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
             Mapper.CreateMap<ProductModel, Product>()
                 .ForMember(dest => dest.Deleted, mo => mo.Ignore());
         }

         public int Order { get; private set; }
    }
}