using Cassandra.Mapping;
using Cassandra;
using Cassandra.Mapping.Attributes;
using GenericTools.Database;

namespace DirectMessages.Models
{
    [Table("user")]
    public class User : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;
    }
    public class UserMapping : Mappings
    {
        public static void UserMap(Cassandra.ISession session)
        {
            try
            {
                session.Execute("CREATE TYPE user (name text, id uuid);");
            }
            catch (Exception e)
            {

            }
            session.UserDefinedTypes.Define(
               UdtMap.For<User>()
            );
        }
    }
}
