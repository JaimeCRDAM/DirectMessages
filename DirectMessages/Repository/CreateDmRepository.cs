using Cassandra;
using Cassandra.Data.Linq;
using Cassandra.Mapping;
using GenericTools.Database;
using System;
using System.Data;
using System.Linq.Expressions;

namespace DirectMessages.Repository
{
    public class CreateDmRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private Cluster _cluster;
        private Cassandra.ISession _session;
        private Table<T> _table;
        private IMapper _mapper;
        public CreateDmRepository(CassandraBuilder cassandraBuild)
        {
            _cluster = cassandraBuild.myCluster;
            _session = _cluster.Connect();
            _session.CreateKeyspaceIfNotExists("direct_message_channel");
            _session.ChangeKeyspace("direct_message_channel");
            _mapper = new Mapper(_session);

            _table = new Table<T>(_session);
            _table.CreateIfNotExists();
        }

        public void Add(T entity)
        {
            _table.Insert(entity).Execute();
        }

        public Task AddAsync(T entity)
        {
            return Task.Run(() => {
                _table.Insert(entity).Execute();
            });
        }

        public void Delete(T entity)
        {
            _mapper.Delete(entity);
        }

        public Task DeleteAsync(T entity)
        {
            return Task.Run(() => {
                _mapper.Delete(entity);
            });
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _table.Where(predicate).AllowFiltering().Execute();
        }

        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return Task.Run(() => {
                return _table.Where(predicate).AllowFiltering().Execute();
            });
        }

        public IEnumerable<T> GetAll()
        {
            return _table.Select(x => x).Execute();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public T GetById(Guid id)
        {
            return _table.First(x => x.Id == id).Execute();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return _table.First(x => x.Id == id).ExecuteAsync();
        }

        public void Update(T entity)
        {
            _mapper.Update(entity);
        }

        public Task UpdateAsync(T entity)
        {
            return Task.Run(() => {
                _mapper.Update(entity);
            });
        }

    }
}
