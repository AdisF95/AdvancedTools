using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> connections = new List<Node>();
    public Vector3 location;
    public string id;

    private void Start()
    {
        location = transform.position;
        id = name;

        DrawLines();
    }

    protected void DrawLines()
    {
        foreach (Node node in connections)
        {
            GameObject myLine = new GameObject();
            myLine.transform.position = location;
            myLine.AddComponent<LineRenderer>();
            LineRenderer lr = myLine.GetComponent<LineRenderer>();
            lr.material = Resources.Load("Line", typeof(Material)) as Material;
            lr.SetWidth(.05f, .05f);
            lr.SetPosition(0, new Vector3(transform.position.x, -0.3f, transform.position.z));
            lr.SetPosition(1, new Vector3(node.transform.position.x, -0.3f, node.transform.position.z));
        }
    }
}
