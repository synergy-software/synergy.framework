using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Synergy.Contracts;

// ReSharper disable once CheckNamespace
namespace Synergy.Extensions
{
    // (c) Gregory Adam 2009

    //http://www.brpreiss.com/books/opus4/html/page557.html
    /*   and
     * An Introduction to data structures with applications
     *   Jean-Paul Tremblay - Paul G. Sorensen
     *   pages 494-497
     *   
     * Topological sort of a forest of directed acyclic graphs
     * 
     * The algorithm is pretty straight
     * for each node, a list of succesors is built
     * each node contains its indegree  (predecessorCount)
     *
     * (1) create a queue containing the key of every node that has no predecessors
     * (2) while (the queue is not empty)
     *      dequeue()
     *      output the key
     *      remove the key
     *      
     *      for each node in the successorList
     *          decrement its predecessorCount
     *          if the predecessorCount becomes empty, add it to the queue
     * 
     * 
     * (3) if any node left, then there is a least one cycle
     *
     */

    /// <summary>
    /// Class for sorting items topologically.
    /// </summary>
    internal sealed class TopologicalSort<T> where T : IEquatable<T>
    {
        private readonly Dictionary<T, NodeInfo> nodes = new Dictionary<T, NodeInfo>();

        /// <summary>
        /// Adds a node with nodeKey
        /// Does not complain if the node is already present
        /// </summary>
        /// <param name="nodeKey"></param>
        public void Node([NotNull] T nodeKey)
        {
            Fail.IfArgumentNull(nodeKey, nameof(nodeKey));

            if (!this.nodes.ContainsKey(nodeKey))
                this.nodes.Add(nodeKey, new NodeInfo());
        }

        /// <summary>
        /// Add an Edge where successor depends on predecessor
        /// Does not complain if the directed arc is already in
        /// </summary>
        ///<param name="predecessor"></param>
        ///<param name="successor"></param>
        public void Edge([NotNull] T predecessor, [NotNull] T successor)
        {
            // make sure both nodes are there
            this.Node(successor);
            this.Node(predecessor);

            // if successor == predecessor (cycle) fail
            Fail.IfTrue(successor.Equals(predecessor),
                "There is at least one cycle in the graph. It cannot be sorted topologically.");

            List<T> successorsOfPredecessor = this.nodes[predecessor].Successors;

            // if the Edge is already there, keep silent
            if (!successorsOfPredecessor.Contains(successor))
            {
                // add the sucessor to the predecessor's successors
                successorsOfPredecessor.Add(successor);

                // increment predecessorrCount of successor
                this.nodes[successor].PredecessorCount++;
            }
        }

        /// <summary>
        /// Sorts topologically the elements added to this class and stores the sorted items in the Queue.
        /// </summary>
        [NotNull, Pure]
        public Queue<T> Sort()
        {
            var sortedQueue = new Queue<T>(); // create, even if it stays empty

            var outputQueue = new Queue<T>(); // with predecessorCount == 0

            // (1) go through all the nodes
            //		if the node's predecessorCount == 0
            //			add it to the outputQueue
            foreach (KeyValuePair<T, NodeInfo> kvp in this.nodes)
                if (kvp.Value.PredecessorCount == 0)
                    outputQueue.Enqueue(kvp.Key);

            // (2) process the output Queue
            //	output the key
            //	delete the key from Nodes
            //	foreach successor
            //		decrement its predecessorCount
            //		if it becomes zero
            //			add it to the output Queue

            while (outputQueue.Count != 0)
            {
                T nodeKey = outputQueue.Dequeue();

                sortedQueue.Enqueue(nodeKey); // add it to sortedQueue

                NodeInfo nodeInfo = this.nodes[nodeKey];

                this.nodes.Remove(nodeKey); // remove it from Nodes

                foreach (T successor in nodeInfo.Successors)
                    if (--this.nodes[successor].PredecessorCount == 0)
                        outputQueue.Enqueue(successor);

                nodeInfo.Clear();
            }

            // outputQueue is empty here
            if (this.nodes.Count != 0)
            {
                //// there is at least one cycle
                throw Fail.Because("There is at least one cycle in the graph. It cannot be sorted topologically.");
            }

            return sortedQueue;
        }

        private class NodeInfo
        {
            public readonly List<T> Successors = new List<T>();

            public int PredecessorCount;

            // Clear NodeInfo
            public void Clear()
            {
                this.Successors.Clear();
            }
        }
    }
}