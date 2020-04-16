using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        this.GetComponent<TextMesh>().text = this.transform.parent.name;
    }
}
