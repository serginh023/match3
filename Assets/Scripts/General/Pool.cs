using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject jewel;
    private readonly List<GameObject> list = new();
    private const int Total = 30;

    private void Start()
    {
        StartPool();
    }

    private void StartPool()
    {
        for(var i = 0; i < Total; i++)
        {
            CreateObject();
        }
    }

    private void CreateObject()
    {
        var go = Instantiate(jewel, Vector3.zero, Quaternion.identity);
        go.transform.SetParent(transform);
        go.SetActive(false);
        list.Add(go);
    }

    public GameObject Get()
    {
        if(list.Count <= 0)
        {
            ExpandList();
        }
        var aux = list[^1];
        list.RemoveAt(list.Count-1);
        aux.SetActive(true);
        return aux;
    }
    
    public void Retrieve(GameObject item)
    {
        list.Add(item);
        item.SetActive(false);
    }

    private void ExpandList()
    {
        CreateObject();
    }

    private void RemoveAll()
    {
        for(var i = list.Count; i > 0; i--)
        {
            list.RemoveAt(i);
        }
    }
}