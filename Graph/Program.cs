using Matrix;

namespace Graph
{
    internal class Program
    {
        private static readonly string FilesPath = $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}\\test_data";
        
        public static void Main(string[] args)
        {
            var rw = new ReaderWriter<int>();
            var matrix = rw.ReadFile($"{FilesPath}\\matrix.txt");
            
            Console.WriteLine("Adjacency matrix:");
            GraphHelper.PrintMatrix(matrix);

            var graph = GraphHelper.CreateGraph(matrix);
            var tryFunc = GraphHelper.ShortestPathDijkstra(graph, matrix, 0);
            GraphHelper.CreateDotFileWithShortPath($"{FilesPath}\\graph.dot", graph, tryFunc);
            GraphHelper.DotToPdf($"{FilesPath}\\graph.dot", $"{FilesPath}\\graph.pdf");
        }
    }
}