using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class Recursive : MonoBehaviour
{
    public Text starting, ending;
    public Text result;

    private List<Node> pathNodes = new List<Node>();
    private bool firstPath;
    private string results = "Path: ";
    private int loops = 0;
    private Node begin, end;

    readonly Stopwatch sw = new Stopwatch();

    protected List<Node> Generate(Node start, Node goal)
    {
        firstPath = true;
        Recursion(start, goal, pathNodes);

        if (loops > 0)
        {
            loops--;
            pathNodes.Clear();
            Generate(begin, end);
            return null;
        }
        else
        {
            foreach (Node city in pathNodes)
            {
                results = results + city.name + "  ";
            }
            result.text = results;
            sw.Stop();
            UnityEngine.Debug.Log("Recurvise took " + sw.ElapsedMilliseconds + " milliseconds");
            sw.Reset();
            return pathNodes;
        }
    }

    private void Recursion(Node currentNode, Node goal, List<Node> oldList)
    {
        List<Node> history = new List<Node>(oldList);
        history.Add(currentNode);

        if (currentNode == goal)
        {

            if (firstPath == true)
            {
                pathNodes = history;
                firstPath = false;
            }
            else if (firstPath == false && pathNodes.Count > history.Count)
            {
                pathNodes = history;
            }
            else
            {
                //Longer Path Found
            }
        }
        else
        {
            for (int i = 0; i < currentNode.connections.Count; i++)
            {
                if (history.Contains(currentNode.connections[i])) continue;

                Recursion(currentNode.connections[i], goal, history);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "City")
                {
                    begin = hit.transform.gameObject.GetComponent<Node>();
                    starting.text = "Start: " + hit.transform.name;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "City")
                {
                    end = hit.transform.gameObject.GetComponent<Node>();
                    ending.text = "Goal: " + hit.transform.name;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            pathNodes.Clear();
            results = "Path: ";
            result.text = "Result";
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
