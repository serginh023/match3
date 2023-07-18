using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject jewel;
    private List<GameObject> list = new List<GameObject>();
    private int total = 30;

    private void Start()
    {
        StartPool();
    }

    private void StartPool()
    {
        for(int i = 0; i < total; i++)
        {
            CreateObject();
        }
    }

    private void CreateObject()
    {
        GameObject go = Instantiate(jewel, Vector3.zero, Quaternion.identity);
        go.transform.SetParent(transform);
        list.Add(go);
    }

    public GameObject Get()
    {
        if(list.Count <= 0)
        {
            Expandlist();
        }
        GameObject aux = list[list.Count-1];
        list.RemoveAt(list.Count-1);
        return aux;
    }
    
    public void Retrieve(GameObject item)
    {
        list.Add(item);
    }

    private void Expandlist()
    {
        CreateObject();
    }

    private void RemoveAll()
    {
        for(int i = list.Count; i > 0; i--)
        {
            list.RemoveAt(i);
        }
    }
}