using Gremlin.Net.Process.Traversal;
using static GremlinUtils.Server;

namespace AppDevs
{
    /// <summary>
    /// Assumes a Gremlin server is running on localhost and listening
    /// on 8182. It builds the server with an empty graph for building
    /// examples easily.
    /// </summary>
    public class GraphBuilder
    {
        public static GraphTraversalSource BuildEmpty() => G();
        
        public static GraphTraversalSource BuildAppDev()
        {
            var g = BuildEmpty();
            return g;
        }
    }
}