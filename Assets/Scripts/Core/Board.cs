using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform m_emptySprite;

    public int m_height = 9;
    public int m_width = 9;
    public int m_header = 1;

    public Transform[,] m_grid;

    public void DrawEmptyCells()
    {
        m_grid = new Transform[m_width, m_height];

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
            Debug.LogWarning("WARNING! Please assign the emptySprite object!");
    }

    bool IsWithinBoard(int x, int y)
    {
        return (x >= 0 && x < m_width && y >= 0);
    }

    public bool isOccupied(int x, int y/*, Piece piece*/)
    {
        return (m_grid[x, y] != null /*&& m_grid[x, y] != piece.transform*/);
    }

    public void StorePieceInGrid(Piece piece)
    {
        if (piece == null)
            return;
        else
        {
            Vector3 pos = piece.transform.position;
            piece.transform.parent = transform;
            m_grid[(int)pos.x, (int)pos.y] = piece.transform;
        }
    }

    public bool IsValidPosition(Piece piece)
    {
        Vector3 position = piece.transform.position;

        if (!IsWithinBoard((int)position.x, (int)position.y))
        {
            return false;
        }

        if (isOccupied((int)position.x, (int)position.y))
        {
            return false;
        }

        return true;
    }

    public bool VerifyCouple(Vector3 piece1, Vector3 piece2)
    {

        if (Vector3.Distance(piece1, piece2) != 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void VerifyGrid()
    {
        for(int i = 0; i < m_height - m_header; i++)
        {
            VerifyRow(i);
        }
    }

    public void VerifyRow(int y)
    {
        int elementCounter = 0;
        List<Vector2> positionsToDelete = new List<Vector2>();

        for ( int i = 0; i < m_width - 1; i++ )
            if (m_grid[i, y] != null)
            {
                positionsToDelete.Add(m_grid[i, y].position);
                elementCounter = 1;

                for (int j = i + 1; j < m_width; j++)
                    if(m_grid[j, y] != null && m_grid[i, y] != null)
                        if ( m_grid[i, y].name.Equals(m_grid[j, y].name) )
                        {
                            elementCounter++;
                            positionsToDelete.Add(m_grid[j, y].position);
                        }
                        else
                            break;

                if (elementCounter >= 3)
                    DeleteCombination(positionsToDelete);

                positionsToDelete.Clear();
            }
        
    }

    private void DeleteCombination(List<Vector2> positions)
    {
        foreach(Vector2 position in positions)
        {
            //Debug.Log("deletando: " + position.x + ", " + position.y + " " + m_grid[(int)position.x, (int)position.y].gameObject.name);
            Destroy(m_grid[(int) position.x, (int) position.y].gameObject);
            m_grid[(int)position.x, (int)position.y] = null;
        }
    }

}
