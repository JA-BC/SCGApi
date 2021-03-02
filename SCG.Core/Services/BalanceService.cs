using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
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
    public class BalanceService: IServiceMethod<BalanceModel>
    {
        private readonly SCGDb _db;
        private readonly IMapper _mapper;

        public BalanceService(SCGDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<BalanceModel> Add(BalanceModel model)
        {

            var entity = _mapper.Map<BalanceModel, BalanceEntity>(model);
            await _db.Balances.AddAsync(entity);
            await _db.SaveChangesAsync();

            return await Requery(x => x.Id == entity.Id);
        }

        public async Task<BalanceModel> Delete(BalanceModel model)
        {
            var item = await _db.Balances.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (item == null)
                throw new Exception("Registro no existente");

            _db.Balances.Remove(item);

            await _db.SaveChangesAsync();

            return model;
        }

        public async Task<IList<BalanceModel>> Select()
        {
            var query = _db.Balances.ProjectTo<BalanceModel>(_mapper.ConfigurationProvider);
            return await query.ToListAsync();
        }

        public async Task<BalanceModel> Requery(Func<BalanceModel, bool> predicate)
        {
            return (await Select()).Where(predicate).FirstOrDefault();
        }

        public async Task<BalanceModel> Update(BalanceModel model)
        {
            var item = await _db.Balances.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (item == null)
                throw new Exception("Registro no encontrado");

            _mapper.Map(model, item);

            await _db.SaveChangesAsync();

            return await Requery(m => m.Id == item.Id);
        }

    }
}
