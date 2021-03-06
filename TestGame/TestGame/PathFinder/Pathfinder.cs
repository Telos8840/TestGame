﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TestGame
{
    // Reresents one node in the search space
    public class SearchNode
    {
        public Point Position;
        public bool Walkable;
        public SearchNode[] Neighbors;
        public SearchNode Parent;
        public bool InOpenList;
        public bool InClosedList;
        public float DistanceToGoal;
        public float DistanceTraveled;
    }

    public class Pathfinder
    {
        // Stores an array of the walkable search nodes.
        private SearchNode[,] searchNodes;

        private int levelWidth;
        private int levelHeight;

        // Holds search nodes that are avaliable to search.
        private List<SearchNode> openList = new List<SearchNode>();
        // Holds the nodes that have already been searched.
        private List<SearchNode> closedList = new List<SearchNode>();

        public Pathfinder(PlaygroundScene map)
        {
            levelWidth = map.Width;
            levelHeight = map.Height;

            InitializeSearchNodes(map);
        }

        // Returns an estimate of the distance between two points.
        private float Heuristic(Point point1, Point point2)
        {
            return Math.Abs(point1.X - point2.X) +
                   Math.Abs(point1.Y - point2.Y);
        }

        // Splits our level up into a grid of nodes.
        private void InitializeSearchNodes(PlaygroundScene map)
        {
            searchNodes = new SearchNode[levelWidth, levelHeight];

            // creates a search node for each of the tiles in our map
            for (int x = 0; x < levelWidth; x++)
            {
                for (int y = 0; y < levelHeight; y++)
                {
                    //Create a search node to represent this tile.
                    SearchNode node = new SearchNode();
                    node.Position = new Point(x, y);

                    // Player can only walk on grass tiles.
                    node.Walkable = map.GetIndex(x, y) == 0;

                    // only store nodes that can be walked on.
                    if (node.Walkable == true)
                    {
                        node.Neighbors = new SearchNode[4];
                        searchNodes[x, y] = node;
                    }
                }
            }

            // for each of the search nodes connects it to each of its neighbours.
            for (int x = 0; x < levelWidth; x++)
            {
                for (int y = 0; y < levelHeight; y++)
                {
                    SearchNode node = searchNodes[x, y];

                    //only want to look at the nodes that are walkable
                    if (node == null || node.Walkable == false)
                    {
                        continue;
                    }

                    // An array of all of the possible neighbors this node could have.
                    Point[] neighbors = new Point[]
                    {
                        new Point (x, y - 1), // The node above the current node
                        new Point (x, y + 1), // The node below the current node.
                        new Point (x - 1, y), // The node left of the current node.
                        new Point (x + 1, y), // The node right of the current node
                    };

                    // loop through each of the possible neighbors
                    for (int i = 0; i < neighbors.Length; i++)
                    {
                        Point position = neighbors[i];

                        // Check if neighbor is part of the level.
                        if (position.X < 0 || position.X > levelWidth - 1 ||
                            position.Y < 0 || position.Y > levelHeight - 1)
                        {
                            continue;
                        }

                        SearchNode neighbor = searchNodes[position.X, position.Y];

                        // Keeps a reference to the nodes that can be walked on.
                        if (neighbor == null || neighbor.Walkable == false)
                        {
                            continue;
                        }

                        // Store a reference to the neighbor.
                        node.Neighbors[i] = neighbor;
                    }
                }
            }
        }

        // Resets the state of the search nodes.
        private void ResetSearchNodes()
        {
            openList.Clear();
            closedList.Clear();

            for (int x = 0; x < levelWidth; x++)
            {
                for (int y = 0; y < levelHeight; y++)
                {
                    SearchNode node = searchNodes[x, y];

                    if (node == null)
                    {
                        continue;
                    }

                    node.InOpenList = false;
                    node.InClosedList = false;

                    node.DistanceTraveled = float.MaxValue;
                    node.DistanceToGoal = float.MaxValue;
                }
            }
        }

        // Use the parent field of the search nodes to trace a path from the end node to the start node.
        private List<Vector2> FindFinalPath(SearchNode startNode, SearchNode endNode)
        {
            closedList.Add(endNode);

            SearchNode parentTile = endNode.Parent;

            // Trace back through the nodes using the parent fieldsto find the best path.
            while (parentTile != startNode)
            {
                closedList.Add(parentTile);
                parentTile = parentTile.Parent;
            }

            List<Vector2> finalPath = new List<Vector2>();

            // Reverse the path and transform into world space.
            for (int i = closedList.Count - 1; i >= 0; i--)
            {
                finalPath.Add(new Vector2(closedList[i].Position.X * 32,
                                          closedList[i].Position.Y * 32));
            }

            return finalPath;
        }

        // Returns the node with the smallest distance to goal.
        private SearchNode FindBestNode()
        {
            SearchNode currentTile = openList[0];

            float smallestDistanceToGoal = float.MaxValue;

            // Find the closest node to the goal.
            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].DistanceToGoal < smallestDistanceToGoal)
                {
                    currentTile = openList[i];
                    smallestDistanceToGoal = currentTile.DistanceToGoal;
                }
            }
            return currentTile;
        }

        // Finds the optimal path from one point to another.
        public List<Vector2> FindPath(Point startPoint, Point endPoint)
        {
            // Only try to find a path if the start and end points are different.
            if (startPoint == endPoint)
            {
                return new List<Vector2>();
            }
            
            //clear previous nodes
            ResetSearchNodes();

            //Store references to the start and end nodes for convenience.
            SearchNode startNode = searchNodes[startPoint.X, startPoint.Y];
            SearchNode endNode = searchNodes[endPoint.X, endPoint.Y];

            //Find estimated distance
            startNode.InOpenList = true;

            startNode.DistanceToGoal = Heuristic(startPoint, endPoint);
            startNode.DistanceTraveled = 0;

            openList.Add(startNode);

            //loop through all the open nodes
            while (openList.Count > 0)
            {
                //Find the smallest/best value
                SearchNode currentNode = FindBestNode();

                //if no path can be found then terminate
                if (currentNode == null)
                {
                    break;
                }

                //return the path
                if (currentNode == endNode)
                {
                    // Trace our path back to the start.
                    return FindFinalPath(startNode, endNode);
                }

                //check the nodes neighbors
                for (int i = 0; i < currentNode.Neighbors.Length; i++)
                {
                    SearchNode neighbor = currentNode.Neighbors[i];

                    //checks it neighbor is walkable
                    if (neighbor == null || neighbor.Walkable == false)
                    {
                        continue;
                    }

                    //get new value for neighbor node
                    float distanceTraveled = currentNode.DistanceTraveled + 1;

                    // An estimate of the distance from this node to the end node.
                    float heuristic = Heuristic(neighbor.Position, endPoint);

                    // If the neighbouring node is not in either the Open List or the Closed List : 
                    if (neighbor.InOpenList == false && neighbor.InClosedList == false)
                    {
                        // (1) Set the neighbouring node’s G value to the G value we just calculated.
                        neighbor.DistanceTraveled = distanceTraveled;
                        // (2) Set the neighbouring node’s F value to the new G value + the estimated 
                        //     distance between the neighbouring node and goal node.
                        neighbor.DistanceToGoal = distanceTraveled + heuristic;
                        // (3) Set the neighbouring node’s Parent property to point at the Active Node.
                        neighbor.Parent = currentNode;
                        // (4) Add the neighbouring node to the Open List.
                        neighbor.InOpenList = true;
                        openList.Add(neighbor);
                    }
                    // Else if the neighbouring node is in either the Open List or the Closed List :
                    else if (neighbor.InOpenList || neighbor.InClosedList)
                    {
                        if (neighbor.DistanceTraveled > distanceTraveled)
                        {
                            neighbor.DistanceTraveled = distanceTraveled;
                            neighbor.DistanceToGoal = distanceTraveled + heuristic;

                            neighbor.Parent = currentNode;
                        }
                    }
                }

                // Remove the Active Node from the Open List and add it to the Closed List
                openList.Remove(currentNode);
                currentNode.InClosedList = true;
            }

            // No path could be found.
            return new List<Vector2>();
        }
    }
}
