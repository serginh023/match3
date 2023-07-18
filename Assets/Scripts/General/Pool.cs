using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject jewel;
    private List<GameObject> list = new List<GameObject>();
    private int total = 10;

    void Start()
    {
        StartPool();
    }

    private void StartPool()
    {
        for(int i = 0; i < total; i++)
        {
            GameObject go = Instantiate(jewel, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(transform);
            list.Add(go);
        }
    }

    public GameObject Get()
    {
        if(list.Count <= 0)
        {
            Expandlist();
        }
        
        return list[list.Count-1];

    }
    
    public void Retrieve(GameObject item)
    {
        list.Add(item);
    }

    private void Expandlist()
    {
        GameObject go = Instantiate(jewel, Vector3.zero, Quaternion.identity);
        list.Add(go);
    }

    private void RemoveAll()
    {
        for(int i = list.Count; i > 0; i--)
        {
            list.RemoveAt(i);
        }
    }
}
