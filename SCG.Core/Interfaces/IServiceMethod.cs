using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.Utilities.Http;

namespace SCG.Core.Interfaces
{
    public interface IServiceMethod<TModel>
    {
        TModel Add(TModel model);
        TModel Delete(TModel model);
        TModel Update(TModel model);
        IQueryable<TModel> Select();
        TModel Requery(Func<TModel, bool> predicate);
        List<TModel> GetPage(APIRequest request);

    }
}
