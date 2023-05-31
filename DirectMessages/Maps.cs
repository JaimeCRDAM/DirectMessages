using Cassandra.Mapping;
using DirectMessages.Models;

namespace DirectMessages
{
    public class MyMappings: Mappings
    {
        public static void mapClasses(Cassandra.ISession session) 
        {
            UserMapping.UserMap(session);
        }
        public MyMappings()
        {
            // Define mappings in the constructor of your class
            // that inherits from Mappings
            For<User>()
               .TableName("user")
               .PartitionKey(u => u.Id)
               .Column(u => u.Id, cm => cm.WithName("id"))
               .Column(u => u.Name, cm => cm.WithName("name"));
        }
    }
}
