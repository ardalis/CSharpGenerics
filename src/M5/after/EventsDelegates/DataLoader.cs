using Repositories;
using System.Collections.Generic;

namespace ClassesMethods
{
    public class DataLoader<T>
    {
        private readonly IWriteRepository<T> _repo;
        private int counter = 0;
        public DataLoader(IWriteRepository<T> repository)
        {
            _repo = repository;
            _repo.EntityAdded += _repo_EntityAdded;
        }

        public int Counter => counter;

        private void _repo_EntityAdded(object sender, T e)
        {
            counter++;
        }

        public void Load(IEnumerable<T> data)
        {
            foreach (var entity in data)
            {
                _repo.Add(entity);
            }
        }
    }
}
