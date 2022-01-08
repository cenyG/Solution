using System;
using System.IO;
using Graph;
using Matrix;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class GraphTest
    {
        private readonly string? _testDir = $"{Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory)}\\..\\..\\test_data";
        
        [Test]
        public void IntGraph()
        {
            var tmpDotFile = $"{_testDir}\\graph.dot";
            var tmpPdfFile = $"{_testDir}\\graph.pdf";
            
            var rw = new ReaderWriter<int>();
            var matrix = rw.ReadFile($"{_testDir}\\adjacency_matrix.txt");

            Console.WriteLine("Adjacency matrix:");
            GraphHelper.PrintMatrix(matrix);

            var graph = GraphHelper.CreateGraph(matrix);
            var tryFunc = GraphHelper.ShortestPathDijkstra(graph, matrix, 0);
            GraphHelper.CreateDotFileWithShortPath(tmpDotFile, graph, tryFunc);
            GraphHelper.DotToPdf(tmpDotFile, tmpPdfFile);
        }
        
        [Test]
        public void DoubleGraph()
        {
            var tmpDotFile = $"{_testDir}\\graph_double.dot";
            var tmpPdfFile = $"{_testDir}\\graph_double.pdf";
            
            var rw = new ReaderWriter<double>();
            var matrix = rw.ReadFile($"{_testDir}\\adjacency_matrix_double.txt");
            

            Console.WriteLine("Adjacency matrix:");
            GraphHelper.PrintMatrix(matrix);

            var graph = GraphHelper.CreateGraph(matrix);
            var tryFunc = GraphHelper.ShortestPathDijkstra(graph, matrix, 0);
            GraphHelper.CreateDotFileWithShortPath(tmpDotFile, graph, tryFunc);
            GraphHelper.DotToPdf(tmpDotFile, tmpPdfFile);
        }
        
        [Test]
        public void TransitiveClosure()
        {
            var tmpDotFile = $"{_testDir}\\graph_transitive.dot";
            var tmpPdfFile = $"{_testDir}\\graph_transitive.pdf";
            
            var rw = new ReaderWriter<int>();
            var matrix = rw.ReadFile($"{_testDir}\\adjacency_matrix.txt");
            
            Console.WriteLine("Adjacency matrix:");
            GraphHelper.PrintMatrix(matrix);

            var transitiveClosureGraph = GraphHelper.TransitiveClosure(matrix);
            Console.WriteLine("Transitive closure:");
            GraphHelper.PrintMatrix(transitiveClosureGraph);

            var graph = GraphHelper.CreateGraph(transitiveClosureGraph);
            GraphHelper.CreateDotFile(tmpDotFile, graph);
            GraphHelper.DotToPdf(tmpDotFile, tmpPdfFile);
        }
        
        [Test]
        public void ShortestPathMatrixTest()
        {
            var rw = new ReaderWriter<int>();
            var matrix = rw.ReadFile($"{_testDir}\\adjacency_matrix.txt");


            Console.WriteLine("Adjacency matrix:");
            GraphHelper.PrintMatrix(matrix);

            var shortestPathMatrix = GraphHelper.FloydWarshall(matrix);
            Console.WriteLine("Shortest path matrix:");
            GraphHelper.PrintMatrix(shortestPathMatrix);
        }
    }
}