module FSharpMatrix.Graph

open QuickGraph
open QuickGraph.Algorithms



 let adjacencyGraph (mx: _[,]) =
     let len = mx.GetLength 0
     let ag = AdjacencyGraph<int, Edge<int>>()
     
     for i in 0 .. len - 1 do
        for j in 0 .. len - 1 do
            if mx.[i,j] > 0 then ag.AddVerticesAndEdge(Edge(i,j)) |> ignore
     ag

let shortestPathDijkstra (ag: AdjacencyGraph<int, Edge<int>>) (mx: _[,]) fromVertex = ag.ShortestPathsDijkstra((fun edge -> mx.[edge.Source, edge.Target]), fromVertex)
    
    
//let createDotFileWithShortPath filePath (ag: AdjacencyGraph<int, Edge<int>>) (tryGetPath: TryFunc<int, IEnumerable<Edge<int>>>) =
//    let shortestPath = HashSet
//    let vxs = ag.Vertices
//    let edges = ag.Edges
//    
//    for vx in vxs do
//        let mutable tmpEdges
//        tryGetPath vx tmpEdges