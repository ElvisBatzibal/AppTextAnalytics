using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppTextAnalytics.Interfaces
{
    public interface IAccess<T>
    {
        Task<bool> InsertItem(T item);
        Task<bool> InsertItems(IEnumerable<T> items);
        Task<List<T>> GetAllItems();
        Task<T> GetItem(string pk);
        Task<bool> AddItem(T item);
        Task<bool> UpdateItem(T item);
        Task<bool> DeleteItem(T item);
    }
}
