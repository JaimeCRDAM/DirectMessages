﻿using Cassandra;

namespace DirectMessages
{
    public class CassandraBuilder
    {
        public Cluster myCluster;
        public CassandraBuilder()
        {
            myCluster = Cluster.Builder()
                .AddContactPoint("172.17.0.2")
                .Build();
        }
    }
}
