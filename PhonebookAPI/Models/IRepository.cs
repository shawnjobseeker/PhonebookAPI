using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhonebookAPI.Models
{
    public interface IRepository
    {
        void Add(Entry entry);
        IEnumerable<Entry> GetAll();
        Entry Find(long key);
        void Remove(long key);
        void Update(Entry entry);
    }
}
