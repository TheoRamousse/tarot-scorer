using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared
{
    public interface IDataManager<TModel> : IDisposable
    {
        Task<TModel> Insert(TModel item);

        Task<TModel> Update(object id, TModel item);

        Task<bool> AddRange(params TModel[] items);

        Task<TModel> FindById(object id);

        Task<IEnumerable<TModel>> GetItems(int index, int count);

        Task Delete(object id);

        Task Clear();

        Task<int> Count();
    }
}
