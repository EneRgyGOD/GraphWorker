﻿namespace GraphWorker
{
	public class Program
	{
		public static void Main()
		{
			List<Graph> graphs = new();

			graphs.Add(new Graph("baba"));

			graphs[0].AddNode(20);
			graphs[0].AddNode(20);
			graphs[0].AddNode(20);
			graphs[0].AddEdge(0, 1, true);
			graphs[0].AddEdge(1, 2, false);
			graphs[0].AddEdge(0, 2, false);

			Console.WriteLine(graphs[0].GetGraphType());

			while (true)
			{
				string? input = Console.ReadLine();

				if (input == null) continue;
				switch (input)
				{
					case "help": Help(); break;
					case "exit": return;
				}

				string[] inputSplitted = input.Split(' ');

				switch (inputSplitted[0])
				{
					case "add": graphs = Add(graphs, inputSplitted); break;
					case "delete": graphs = Delete(graphs, inputSplitted); break;
					case "show": Show(graphs, inputSplitted); break;
				}
			}
		}

		static void Help()
		{

			Console.WriteLine("\nadd\n\tgraph {name}\n\tnode {graphName} {Value}\n\tedge {graphName} {from} {to} {is directed (0 or 1)}\n" +
				"delete\n\tgraph {name}\n\tnode {graphName} {position}\n\tedge {graphName} {from} {to}\n" +
				"show\n\tmatrix {graphName}\n\tgraphs\n\tedges {graphName}\n\tnodes {graphName}\n\tgraphType {graphName}");
		}

		private static List<Graph> Add(List<Graph> graphs, string[] input)
		{
			if (input == null || graphs == null) return null!;

			try
			{
				switch (input[1])
				{
					case "graph":
						{
							if (graphs.Any(x => x.Label == input[2])) { Console.WriteLine("This name is alredy taken"); break; }
							graphs.Add(new Graph(input[2]));
							Console.WriteLine($"Added graph {input[2]}");
							break;
						}

					case "node": graphs.Find(x => x.Label == input[2])!.AddNode(Convert.ToInt32(input[3])); Console.WriteLine($"Added node to {input[2]} with value {input[3]}"); break;
					case "edge": graphs.Find(x => x.Label == input[2])!.AddEdge(Convert.ToInt32(input[3]) - 1, Convert.ToInt32(input[4]) - 1, Convert.ToBoolean(input[5])); Console.WriteLine($"Added edge to {input[2]} from {input[3]} to {input[4]}"); break;
				}
			}
			catch (IndexOutOfRangeException)
			{
				Console.WriteLine("Some index is out of range");
			}
			catch
			{
				Console.WriteLine("Something went wrong, please re-check your input!");
				Help();
			}

			return graphs;
		}

		private static List<Graph> Delete(List<Graph> graphs, string[] input)
		{
			if (input == null || graphs == null) return null!;

			try
			{
				switch (input[1])
				{
					case "graph":
						{
							if (!graphs.Any(x => x.Label == input[2])) { Console.WriteLine("This name doesn't exist"); break; }
							graphs.RemoveAt(graphs.FindIndex(x => x.Label == input[2]));
							Console.WriteLine($"Deleted graph {input[2]}");
							break;
						}
					case "node": graphs.Find(x => x.Label == input[2])!.RemoveNode(Convert.ToInt32(input[3]) - 1); Console.WriteLine($"Deleted node in {input[2]}"); break;
					case "edge": graphs.Find(x => x.Label == input[2])!.RemoveEdge(Convert.ToInt32(input[3]) - 1, Convert.ToInt32(input[4]) - 1); Console.WriteLine($"Deleted edge in {input[2]} from {input[3]}, to {input[4]}"); break;
				}
			}
			catch (IndexOutOfRangeException)
			{
				Console.WriteLine("Some index is out of range");
			}
			catch
			{
				Console.WriteLine("Something went wrong, please re-check your input!");
				Help();
			}

			return graphs;
		}

		private static void Show(List<Graph> graphs, string[] input)
		{
			if (input == null || graphs == null) return;
			
			try
			{
				switch (input[1])
				{
					case "matrix": graphs.Find(x => x.Label == input[2])!.ShowAdjacencyMatrix(); break;
					case "graphs": graphs.ForEach(x => Console.WriteLine(x.Label)); break;
					case "nodes": graphs.Find(x => x.Label == input[2])!.ShowNodes(); break;
					case "edges": graphs.Find(x => x.Label == input[2])!.ShowEdges(); break;
					case "graphType": graphs.Find(x => x.Label == input[2])!.GetGraphType(); break;
				}
			}
			catch
			{
				Console.WriteLine("Something went wrong, please re-check your input!");
				Help();
			}
		}
	}
}