using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class BFS : MonoBehaviour
{
    public Text result;

    private List<BFSNode> pathNodes = new List<BFSNode>();
    private List<BFSNode> toDoNodes = new List<BFSNode>();
    private List<BFSNode> alreadyBeenNodes = new List<BFSNode>();
    private Dictionary<BFSNode, BFSNode> nodeParent = new Dictionary<BFSNode, BFSNode>();
    private BFSNode begin, end;
    private int loops = 0;

    readonly Stopwatch sw = new Stopwatch();

    protected List<BFSNode> Generate(BFSNode start, BFSNode goal)
    {
        toDoNodes.Add(start);
        nodeParent.Add(start, null);

        while (toDoNodes.Count > 0)
        {
            BFSNode currentNode = toDoNodes[0];

            alreadyBeenNodes.Add(currentNode);
            toDoNodes.Remove(currentNode);

            if (currentNode == goal)
            {
                BFSNode backTrack = goal;
                do
                {
                    pathNodes.Add(backTrack);
                    backTrack = nodeParent[backTrack];
                }
                while (backTrack != null);
                pathNodes.Reverse();

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

            foreach (BFSNode nextNode in currentNode.connections)
            {
                if (toDoNodes.Contains(nextNode) | alreadyBeenNodes.Contains(nextNode))
                {
                    //Nothing
                }
                else
                {
                    toDoNodes.Add(nextNode);
                    nodeParent.Add(nextNode, currentNode);
                }
            }
        }
        return null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftControl))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<BFSNode>() != null)
                {
                    if (begin == null)
                    {
                        begin = hit.transform.gameObject.GetComponent<BFSNode>();
                        hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.green; ;
                    }
                    else
                    {
                        begin.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                        begin = hit.transform.gameObject.GetComponent<BFSNode>();
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
                if (hit.transform.gameObject.GetComponent<BFSNode>() != null)
                {
                    if (end == null)
                    {
                        end = hit.transform.gameObject.GetComponent<BFSNode>();
                        hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red; ;
                    }
                    else
                    {
                        end.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                        end = hit.transform.gameObject.GetComponent<BFSNode>();
                        hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red; ;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach (BFSNode node in pathNodes)
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
