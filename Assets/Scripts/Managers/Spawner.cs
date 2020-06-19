using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Piece[] pieces;
    [SerializeField]
    private Board m_board;


    public void InitalSpawn()
    {
        for(int i = 0; i < m_board.m_width; i++)
            for(int j = 0; j < m_board.m_height; j++)
            {
                
            }
    }


    public GameObject SpawnAtPosition(int x, int y)
    {
        int index = (int) Random.Range(0, pieces.Length);

        GameObject go = Instantiate(piece[index], new Vector3(x, y, 0), Quaternion.identity) ;

        return go;
    }
}
