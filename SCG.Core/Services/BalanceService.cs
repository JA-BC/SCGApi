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

        public BalanceModel Add(BalanceModel model)
        {

            var entity = _mapper.Map<BalanceModel, BalanceEntity>(model);
            _db.Balances.Add(entity);
            _db.SaveChanges();

            return Requery(x => x.Id == entity.Id);
        }

        public BalanceModel Delete(BalanceModel model)
        {
            var item = _db.Balances.FirstOrDefault(x => x.Id == model.Id);

            if (item == null)
                throw new Exception("Registro no existente");

            _db.Balances.Remove(item);

            _db.SaveChanges();

            return model;
        }

        public IQueryable<BalanceModel> Select()
        {
            var query = _db.Balances.ProjectTo<BalanceModel>(_mapper.ConfigurationProvider);
            return query;
        }

        public BalanceModel Requery(Func<BalanceModel, bool> predicate)
        {
            return Select().Where(predicate).FirstOrDefault();
        }

        public BalanceModel Update(BalanceModel model)
        {
            var item = _db.Balances.FirstOrDefault(x => x.Id == model.Id);

            if (item == null)
                throw new Exception("Registro no encontrado");

            _mapper.Map(model, item);

            _db.SaveChanges();

            return Requery(m => m.Id == item.Id);
        }

    }
}
