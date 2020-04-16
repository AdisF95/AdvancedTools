using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform nodeType = null;

    [SerializeField]
    private int width = 7;
    [SerializeField]
    private int height = 5;

    private float spacing = 1.5f;

    private void Awake()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Transform node = Instantiate(nodeType);
                node.parent = transform;
                node.localPosition = new Vector3(i * spacing, 0, j * -spacing);
            }
        }
    }
}
