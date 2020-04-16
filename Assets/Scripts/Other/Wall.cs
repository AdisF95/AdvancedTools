using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                hit.transform.gameObject.GetComponent<BoxCollider>().size = new Vector3(1,1,1);
                hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
            }
        }

        if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftControl))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                hit.transform.gameObject.GetComponent<BoxCollider>().size = new Vector3(1.7f, 1, 1.7f);
                hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.white; ;
            }
        }
    }
}
