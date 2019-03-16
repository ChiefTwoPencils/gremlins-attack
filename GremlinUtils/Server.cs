using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Remote;
using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Process.Traversal.Strategy.Decoration;
using Gremlin.Net.Structure.IO.GraphSON;
using static Gremlin.Net.Process.Traversal.AnonymousTraversalSource;

namespace GremlinUtils
{
    public static class Server
    {
        public static GraphTraversalSource G() => Traversal().WithRemote(LocalRemoteConn);

        public static GraphTraversalSource SubG(SubgraphStrategy strategy) => G().WithStrategies(strategy);

        public static void DisposeConnection() => LocalRemoteConn.Dispose();
        
        private static DriverRemoteConnection LocalRemoteConn { get; } 
            = new DriverRemoteConnection(LocalClient);

        private static GremlinClient LocalClient => new GremlinClient(
            new GremlinServer("localhost", 8182),
            new GraphSON3Reader(),
            new GraphSON3Writer());
    }
}