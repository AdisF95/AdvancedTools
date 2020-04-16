using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class DFS : MonoBehaviour
{
    public Text result;

    private List<DFSNode> pathNodes = new List<DFSNode>();
    private List<DFSNode> toDoNodes = new List<DFSNode>();
    private List<DFSNode> alreadyBeenNodes = new List<DFSNode>();
    private Dictionary<DFSNode, DFSNode> nodeParent = new Dictionary<DFSNode, DFSNode>();
    private DFSNode begin, end;
    private int loops = 0;

    readonly Stopwatch sw = new Stopwatch();

    protected List<DFSNode> Generate(DFSNode start, DFSNode goal)
    {
        toDoNodes.Add(start);
        nodeParent.Add(start, null);

        while (toDoNodes.Count > 0)
        {
            DFSNode currentNode = toDoNodes[toDoNodes.Count - 1];

            alreadyBeenNodes.Add(currentNode);
            toDoNodes.Remove(currentNode);

            if (currentNode == goal)
            {
                DFSNode backTrack = goal;
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

            foreach (DFSNode nextNode in currentNode.connections)
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
                if (hit.transform.gameObject.GetComponent<DFSNode>() != null)
                {
                    if (begin == null)
                    {
                        begin = hit.transform.gameObject.GetComponent<DFSNode>();
                        hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.green; ;
                    }
                    else
                    {
                        begin.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                        begin = hit.transform.gameObject.GetComponent<DFSNode>();
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
                if (hit.transform.gameObject.GetComponent<DFSNode>() != null)
                {
                    if (end == null)
                    {
                        end = hit.transform.gameObject.GetComponent<DFSNode>();
                        hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red; ;
                    }
                    else
                    {
                        end.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                        end = hit.transform.gameObject.GetComponent<DFSNode>();
                        hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red; ;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach (DFSNode node in pathNodes)
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
