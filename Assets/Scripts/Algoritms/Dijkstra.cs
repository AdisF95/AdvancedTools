using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class Dijkstra : MonoBehaviour
{
    public List<DijkstraNode> allNodes = new List<DijkstraNode>();
    public Text result;

    private List<DijkstraNode> pathNodes = new List<DijkstraNode>();
    private int loops = 0;
    private List<DijkstraNode> toDoNodes = new List<DijkstraNode>();
    private List<DijkstraNode> alreadyBeenNodes = new List<DijkstraNode>();
    private Dictionary<DijkstraNode, DijkstraNode> nodeParent = new Dictionary<DijkstraNode, DijkstraNode>();
    private DijkstraNode begin, end;
    
    readonly Stopwatch sw = new Stopwatch();

    private void Start()
    {
        allNodes.AddRange(FindObjectsOfType<DijkstraNode>());
    }

    protected List<DijkstraNode> Generate(DijkstraNode start, DijkstraNode end )
	{
        // If the start and end are same node, we can return the start node
        if ( start == end )
		{
            pathNodes.Add ( start );
			return pathNodes;
		}
		
		// The list of unvisited nodes
		List<DijkstraNode> unvisited = new List<DijkstraNode> ();
		
		// Previous nodes in optimal path from source
		Dictionary<DijkstraNode, DijkstraNode> previous = new Dictionary<DijkstraNode, DijkstraNode> ();
		
		// The calculated distances, set all to Infinity at start, except the start Node
		Dictionary<DijkstraNode, float> distances = new Dictionary<DijkstraNode, float> ();
		
		for ( int i = 0; i < allNodes.Count; i++ )
		{
            DijkstraNode node = allNodes [ i ];
			unvisited.Add ( node );
			
			// Setting the node distance to Infinity
			distances.Add ( node, float.MaxValue );
		}
		
		// Set the starting Node distance to zero
		distances [ start ] = 0f;
		while ( unvisited.Count != 0 )
		{
			
			// Ordering the unvisited list by distance, smallest distance at start and largest at end
			unvisited = unvisited.OrderBy ( node => distances [ node ] ).ToList ();

            // Getting the Node with smallest distance
            DijkstraNode current = unvisited [ 0 ];
			
			// Remove the current node from unvisisted list
			unvisited.Remove ( current );
			
			// When the current node is equal to the end node, then we can break and return the path
			if ( current == end )
			{
				
				// Construct the shortest path
				while ( previous.ContainsKey ( current ) )
				{
					
					// Insert the node onto the final result
					pathNodes.Insert ( 0, current );
					
					// Traverse from start to end
					current = previous [ current ];
				}
				
				// Insert the source onto the final result
				pathNodes.Insert ( 0, current );
				break;
			}
			
			// Looping through the Node connections (neighbors) and where the connection (neighbor) is available at unvisited list
			for ( int i = 0; i < current.connections.Count; i++ )
			{
                DijkstraNode neighbor = current.connections [ i ] as DijkstraNode;
				
				// Getting the distance between the current node and the connection (neighbor)
				float length = Vector3.Distance ( current.transform.position, neighbor.transform.position );
				
				// The distance from start node to this connection (neighbor) of current node
				float alt = distances [ current ] + length;
				
				// A shorter path to the connection (neighbor) has been found
				if ( alt < distances [ neighbor ] )
				{
					distances [ neighbor ] = alt;
					previous [ neighbor ] = current;
				}
			}
		}

        if (loops > 0)
        {
            loops--;
            alreadyBeenNodes.Clear();
            nodeParent.Clear();
            pathNodes.Clear();
            toDoNodes.Clear();
            Generate(begin, end);
            return null;
        }
        else
        {
            for (int i = 1; i < pathNodes.Count - 1; i++)
            {
                pathNodes[i].transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
            }

            sw.Stop();
            result.text = "Result: " + sw.ElapsedMilliseconds + "ms & " + pathNodes.Count + " Nodes";
            sw.Reset();
            return pathNodes;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftControl))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<DijkstraNode>() != null)
                {
                    if (begin == null)
                    {
                        begin = hit.transform.gameObject.GetComponent<DijkstraNode>();
                        hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.green; ;
                    }
                    else
                    {
                        begin.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                        begin = hit.transform.gameObject.GetComponent<DijkstraNode>();
                        hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.green; ;
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && !Input.GetKey(KeyCode.LeftControl))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<DijkstraNode>() != null)
                {
                    if (end == null)
                    {
                        end = hit.transform.gameObject.GetComponent<DijkstraNode>();
                        hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red; ;
                    }
                    else
                    {
                        end.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                        end = hit.transform.gameObject.GetComponent<DijkstraNode>();
                        hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red; ;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach (DijkstraNode node in pathNodes)
            {
                node.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
            }

            alreadyBeenNodes.Clear();
            nodeParent.Clear();
            pathNodes.Clear();
            toDoNodes.Clear();
            result.text = "Result:";
            loops = 0;
        }
    }

    public void OnButtonClick()
    {
        if (begin != null && end != null)
        {
            sw.Start();
            Generate(begin, end);
        }
    }
}
