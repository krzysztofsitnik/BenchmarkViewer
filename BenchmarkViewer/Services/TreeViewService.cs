﻿using BenchmarkViewer.Models.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace BenchmarkViewer.Services
{
    public class TreeViewService
    {
        public static IReadOnlyList<BenchmarkTreeViewModel> BuildTreeView()
        {
            var dataStorageService = new DataStorageService();

            var benchmarks = dataStorageService.GetBenchmarks();

            List<BenchmarkTreeViewModel> topLevelNodes = new List<BenchmarkTreeViewModel>();

            foreach (var item in benchmarks)
            {
                var partsOfName = item.BenchmarkName.Split('.');
                PrepareNodeTree(topLevelNodes, partsOfName, 0, item.BenchmarkId);
            }

            return topLevelNodes;
        }

        private static void PrepareNodeTree(List<BenchmarkTreeViewModel> nodes, string[] partsOfName, int index, int benchmarkId)
        {
            var node = nodes.FirstOrDefault(p => p.Text == partsOfName[index]);

            if (node == null)
            {
                node = new BenchmarkTreeViewModel { Text = partsOfName[index] };
                nodes.Add(node);
            }

            if (partsOfName.Length > index + 1)
            {
                PrepareNodeTree(node.Children, partsOfName, index + 1, benchmarkId);
            }
            else
            {
                node.Id = benchmarkId;
            }
        }
    }
}