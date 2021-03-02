using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.Core.Interfaces
{
    public interface IServiceMethod<TModel>
    {
        Task<TModel> Add(TModel model);
        Task<TModel> Delete(TModel model);
        Task<TModel> Update(TModel model);
        Task<IList<TModel>> Select();
        Task<TModel> Requery(Func<TModel, bool> predicate);

    }
}
