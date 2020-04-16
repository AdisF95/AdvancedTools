using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarNode : BaseNode
{
    public List<BaseNode> connections = new List<BaseNode>();

    private void OnTriggerEnter(Collider other)
    {
        connections.Add(other.gameObject.GetComponent<AStarNode>());
        DrawLines(connections);
    }

    private void OnTriggerExit(Collider other)
    {
        connections.Remove(other.gameObject.GetComponent<AStarNode>());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<AStarNode>() != null)
                {
                    hit.transform.gameObject.GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
                    hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftControl))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                hit.transform.gameObject.GetComponent<BoxCollider>().size = new Vector3(1.7f, 1, 1.7f);
                hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
    }
}
