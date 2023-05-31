using Cassandra;
using Cassandra.Data.Linq;
using Cassandra.Mapping;
using GenericTools.Database;
using System.Linq.Expressions;

namespace DirectMessages.Repository
{
    public class DirectMessageRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private Cluster _cluster;
        private Cassandra.ISession _session;
        private Table<T> _table;
        private IMapper _mapper;
        public DirectMessageRepository(CassandraBuilder cassandraBuild)
        {
            _cluster = cassandraBuild.myCluster;
            _session = _cluster.Connect();
            _session.CreateKeyspaceIfNotExists("direct_message");
            _session.ChangeKeyspace("direct_message");
            _mapper = new Mapper(_session);
            MappingConfiguration.Global.Define<MyMappings>();
            MyMappings.mapClasses(_session);
            _table = new Table<T>(_session);
            _table.CreateIfNotExists();
        }

        public void Add(T entity)
        {
            _table.Insert(entity).Execute();
        }

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            _mapper.Delete(entity);
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _table.Where(predicate).AllowFiltering().Execute();
        }

        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public T GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
