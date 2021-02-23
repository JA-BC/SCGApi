using AutoMapper;
using AutoMapper.QueryableExtensions;
using SCG.Core.Database;
using SCG.Core.Database.Entities;
using SCG.Core.Interfaces;
using SCG.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.Utilities.Http;
using WebApi.Utilities.IQueryableExtensions;

namespace SCG.Core.Services
{
    public class CategoriaService: IServiceMethod<CategoriaModel>
    {
        private readonly SCGDb _db;
        private readonly IMapper _mapper;

        public CategoriaService(SCGDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public CategoriaModel Add(CategoriaModel model)
        {

            var entity = _mapper.Map<CategoriaModel, CategoriaEntity>(model);
            _db.Categorias.Add(entity);
            _db.SaveChanges();

            return Requery(x => x.Id == entity.Id);
        }

        public CategoriaModel Delete(CategoriaModel model)
        {
            var item = _db.Categorias.FirstOrDefault(x => x.Id == model.Id);

            if (item == null)
                throw new Exception("Registro no existente");

            _db.Categorias.Remove(item);

            _db.SaveChanges();

            return model;
        }

        public IQueryable<CategoriaModel> Select()
        {
            var query = _db.Categorias.ProjectTo<CategoriaModel>(_mapper.ConfigurationProvider);
            return query;
        }

        public List<CategoriaModel> GetPage(APIRequest request)
        {
            return Select()
                    .AddFilter(request.Filters)
                    .AddSortBy(request.Sorts)
                    .AddPagination(request.Pagination);
        }

        public CategoriaModel Requery(Func<CategoriaModel, bool> predicate)
        {
            return Select().Where(predicate).FirstOrDefault();
        }

        public CategoriaModel Update(CategoriaModel model)
        {
            var item = _db.Categorias.FirstOrDefault(x => x.Id == model.Id);

            if (item == null)
                throw new Exception("Registro no encontrado");

            _mapper.Map(model, item);

            _db.SaveChanges();

            return Requery(m => m.Id == item.Id);
        }

    }
}
