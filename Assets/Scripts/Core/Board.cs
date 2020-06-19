using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform m_emptySprite;

    public int m_height = 20;
    public int m_width = 10;
    public int m_header = 4;

    Transform[,] m_grid;

    // Start is called before the first frame update
    void Start()
    {
        m_grid = new Transform[m_width, m_height];
        DrawEmptyCells();
    }

    void DrawEmptyCells()
    {
        if (m_emptySprite != null)
            for (int i = 0; i < m_height - m_header; i++)
                for (int j = 0; j < m_width; j++)
                {
                    Transform clone;
                    clone = Instantiate(m_emptySprite, new Vector3(j, i, 0), Quaternion.identity) as Transform;
                    clone.name = "Board Space ( x = " + j.ToString() + ", y = " + i.ToString() + ")";
                    clone.transform.parent = transform;
                }
        else
            Debug.LogWarning("WARNING! PLease assign the emptySprite object!");
    }

    bool IsWithinBoard(int x, int y)
    {
        return (x >= 0 && x < m_width && y >= 0);
    }

    public bool isOccupied(int x, int y, Piece piece)
    {
        return (m_grid[x, y] != null && m_grid[x, y] != piece.transform);
    }




}
