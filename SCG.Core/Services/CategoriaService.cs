using Microsoft.EntityFrameworkCore;
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
using System.Threading.Tasks;

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

        public async Task<CategoriaModel> Add(CategoriaModel model)
        {

            var entity = _mapper.Map<CategoriaModel, CategoriaEntity>(model);
            await _db.Categorias.AddAsync(entity);
            await _db.SaveChangesAsync();

            return await Requery(x => x.Id == entity.Id);
        }

        public async Task<CategoriaModel> Delete(CategoriaModel model)
        {
            var item = await _db.Categorias.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (item == null)
                throw new Exception("Registro no existente");

            _db.Categorias.Remove(item);

            await _db.SaveChangesAsync();

            return model;
        }

        public async Task<IList<CategoriaModel>> Select()
        {
            var query = _db.Categorias.ProjectTo<CategoriaModel>(_mapper.ConfigurationProvider);
            return await query.ToListAsync();
        }

        public async Task<CategoriaModel> Requery(Func<CategoriaModel, bool> predicate)
        {
            return (await Select()).Where(predicate).FirstOrDefault();
        }

        public async Task<CategoriaModel> Update(CategoriaModel model)
        {
            var item = await _db.Categorias.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (item == null)
                throw new Exception("Registro no encontrado");

            _mapper.Map(model, item);

            await _db.SaveChangesAsync();

            return await Requery(m => m.Id == item.Id);
        }

    }
}
