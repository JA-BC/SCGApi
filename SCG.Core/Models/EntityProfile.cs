using AutoMapper;
using SCG.Core.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCG.Core.Models
{
    public class EntityProfile: Profile
    {
        public EntityProfile()
        {
            CreateMap<BalanceModel, BalanceEntity>().ReverseMap();
            CreateMap<CategoriaModel, CategoriaEntity>().ReverseMap();

        }
    }
}
