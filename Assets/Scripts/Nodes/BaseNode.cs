using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNode : MonoBehaviour
{
    public Vector3 location;
    public string id;

    void Start()
    {
        location = transform.position;
        id = name;
    }

    protected void DrawLines(List<BaseNode> connections)
    {
        foreach (BaseNode node in connections)
        {
            GameObject myLine = new GameObject();
            myLine.tag = "Line";
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
