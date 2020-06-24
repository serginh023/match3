using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform m_emptySprite;
    [SerializeField]
    private Spawner m_spawner;
    [SerializeField]
    private ScoreManager m_scoreManager;

    public int m_height = 9;
    public int m_width = 9;
    public int m_header = 1;
    public bool m_won = false;
    public bool m_hasValidChance = false;

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
        return (x >= 0 && x < m_width && y >= 0 && y < m_height - m_header);
    }

    public bool isOccupied(int x, int y)
    {
        return (m_grid[x, y] != null);
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

    public void ErasePosition(int x, int y)
    {
        if (m_grid[x, y] != null) Destroy(m_grid[x, y].gameObject);

        m_grid[x, y] = null;
    }

    public bool IsValidPosition(Piece piece)
    {
        Vector3 position = piece.transform.position;

        if (!IsWithinBoard((int)position.x, (int)position.y))
            return false;

        if (isOccupied((int)position.x, (int)position.y))
            return false;

        return true;
    }

    public bool VerifyCouple(Vector3 piece1, Vector3 piece2)
    {
        if (Vector3.Distance(piece1, piece2) != 1)
            return false;
        else
            return true;
    }

    public void VerifyGridCombination(bool valid = false)
    {
        m_won = false;

        m_hasValidChance = false;

        for (int i = 0; i < m_height - m_header; i++)
            VerifyRowCombination(i, valid);

        if (valid)
        {
            for (int j = 0; j < m_width; j++)
                VerifyColumnCombination(j, valid);
        }
        else if (!m_hasValidChance)
        {
            for (int j = 0; j < m_width; j++)
                VerifyColumnCombination(j, valid);
        }

    }

    public void VerifyRowCombination(int y, bool valid = false)
    {
        List<Vector2> positionsToDelete = new List<Vector2>();

        for ( int i = 0; i < m_width - 1; i++ )
            if (m_grid[i, y] != null)
            {
                positionsToDelete.Add(m_grid[i, y].position);

                for (int j = i + 1; j < m_width; j++)

                    if (m_grid[j, y] != null && m_grid[i, y] != null)
                        if (m_grid[i, y].name.Equals(m_grid[j, y].name))
                            positionsToDelete.Add(m_grid[j, y].position);
                        else
                            break;

                if (positionsToDelete.Count >= 3)
                {
                    if (valid)
                    {
                        DeleteCombination(positionsToDelete);
                        m_scoreManager.Score(positionsToDelete.Count);
                        m_won = true;
                    }
                    else
                    {
                        m_hasValidChance = true;
                        positionsToDelete.Clear();
                        return;
                    }

                }

                positionsToDelete.Clear();
            }
    }

    public void VerifyColumnCombination(int x, bool valid = false)
    {
        List<Vector2> positionsToDelete = new List<Vector2>();

        for (int i = 0; i < m_height - m_header - 1; i++)
            if (m_grid[x, i] != null)
            {
                positionsToDelete.Add(m_grid[x, i].position);

                for (int j = i + 1; j < m_height - m_header; j++)

                    if (m_grid[x, j] != null && m_grid[x, i] != null)
                        if (m_grid[x, i].name.Equals(m_grid[x, j].name))
                            positionsToDelete.Add(m_grid[x, j].position);
                        else
                            break;

                if (positionsToDelete.Count >= 3)
                {
                    if (valid)
                    {
                        DeleteCombination(positionsToDelete);
                        m_scoreManager.Score(positionsToDelete.Count);
                        m_won = true;
                    }
                    else
                    {
                        m_hasValidChance = true;
                        Debug.Log("entrou aqui");
                        positionsToDelete.Clear();
                        return;
                    }
                }

                positionsToDelete.Clear();
            }
    }

    public bool VerifyAnyChance()
    {
        for(int i = 0; i < m_width; i++)
            for (int j = 0; j < m_height - m_header; j++)
            {
                //Verificação para a Direita
                if (IsWithinBoard(i, j + 1))
                {
                    ChangeRight(i, j);

                    VerifyGridCombination();

                    ChangeRight(i, j);

                    if (m_hasValidChance)
                        return true;
                }

                //Verificação para a Esquerda
                if (IsWithinBoard(i, j - 1))
                {
                    ChangeLeft(i, j);

                    VerifyGridCombination();

                    ChangeLeft(i, j);

                    if (m_hasValidChance)
                        return true;
                }

                //Verificação para Cima
                if (IsWithinBoard(i + 1, j))
                {
                    ChangeUp(i, j);

                    VerifyGridCombination();

                    ChangeUp(i, j);

                    if (m_hasValidChance)
                        return true;
                }

                //Verificação para Baixo
                if (IsWithinBoard(i - 1, j))
                {
                    ChangeDown(i, j);

                    VerifyGridCombination();

                    ChangeDown(i, j);

                    if (m_hasValidChance)
                        return true;
                }
            }

        return false;
    }


    void ChangeRight(int x, int y)
    {
        Debug.Log("(" + x + ", " + y + ")");
        Transform aux = m_grid[x, y];

        m_grid[x, y] = m_grid[x, y + 1];
        m_grid[x, y + 1] = aux;
        m_grid[x, y].gameObject.GetComponent<Piece>().MoveLeft();
        m_grid[x, y + 1].gameObject.GetComponent<Piece>().MoveRight();
    }

    void ChangeLeft(int x, int y)
    {
        Debug.Log("(" + x + ", " + y + ")");
        Transform aux = m_grid[x, y];

        m_grid[x, y] = m_grid[x, y - 1];
        m_grid[x, y - 1] = aux;
        m_grid[x, y].gameObject.GetComponent<Piece>().MoveRight();
        m_grid[x, y - 1].gameObject.GetComponent<Piece>().MoveLeft();
    }

    void ChangeUp(int x, int y)
    {
        Debug.Log("(" + x + ", " + y + ")");
        Transform aux = m_grid[x, y];

        m_grid[x, y] = m_grid[x + 1, y];
        m_grid[x + 1, y] = aux;
        m_grid[x, y].gameObject.GetComponent<Piece>().MoveDown();
        m_grid[x + 1, y].gameObject.GetComponent<Piece>().MoveUp();
    }

    void ChangeDown(int x, int y)
    {
        Debug.Log("(" + x + ", " + y + ")");
        Transform aux = m_grid[x, y];

        m_grid[x, y] = m_grid[x - 1, y];
        m_grid[x - 1, y] = aux;
        m_grid[x, y].gameObject.GetComponent<Piece>().MoveUp();
        m_grid[x - 1, y].gameObject.GetComponent<Piece>().MoveDown();
    }



    private void DeleteCombination(List<Vector2> positions)
    {
        foreach(Vector2 position in positions)
        {
            if ( m_grid[(int)position.x, (int)position.y] != null ) Destroy(m_grid[(int) position.x, (int) position.y].gameObject);
            m_grid[(int)position.x, (int)position.y] = null;
        }

        ClearAllNullPieces();
    }


    private void ClearAllNullPieces()
    {
        for (int i = 0; i < m_width; i++)
            for (int j = 0; j < m_height - m_header; j++)
                if (m_grid[i, j] == null)
                {
                    ShiftElementsDown(i, j);
                    j--;
                }
    }

    private void ShiftElementsDown(int i, int j)
    {
        for (int k = j; k < m_height - m_header - 1; k++)
        {
            m_grid[i, k] = m_grid[i, k+1];
            if(m_grid[i, k] != null) m_grid[i, k].transform.position += new Vector3(0, -1, 0);
        }

        //Precisa dar spwan em um novo elemento no topo do grid
        m_grid[i, m_height - m_header - 1] = m_spawner.SpawnAtPosition(new Vector3(i, m_height - m_header - 1, 0)).transform;
    }
}
