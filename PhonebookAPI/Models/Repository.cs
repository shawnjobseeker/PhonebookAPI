using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhonebookAPI.Models
{
    public class Repository : IRepository
    {
        private readonly Context context;
        public Repository(Context context)
        {
            this.context = context;
            if (this.context.Entries.Count() == 0)
                Add(new Entry { Name = "Shawn" });
        }
        public void Add(Entry entry)
        {
            context.Entries.Add(entry);
            context.SaveChanges();
        }
        public IEnumerable<Entry> GetAll()
        {
            return context.Entries.ToList();
        }
        public Entry Find(long key)
        {
            return context.Entries.FirstOrDefault(t => t.Key == key);
        }
        public void Remove(long key)
        {
            var entry = context.Entries.First(t => t.Key == key);
            context.Entries.Remove(entry);
            context.SaveChanges();
        }
        public void Update(Entry entry)
        {
            context.Entries.Update(entry);
            context.SaveChanges();
        }
    }
}
