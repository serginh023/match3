using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private int collumns;
    [SerializeField] private int rows;
    [SerializeField] private ButtonCandy buttonCandy;
    [SerializeField] private Cell cell;
    [SerializeField] private Dictionary<Vector2Int, Cell> cellList;

    private void Start()
    {   
        PopulateWall();
    }

    private void PopulateWall()
    {
        for(int i = 0; i < rows; i++)
            for(int j = 0; j < collumns; j++)
            {
                Cell NewCell = Instantiate(cell);
                NewCell.transform.SetParent(transform);
                NewCell.SetPosition(new Vector3(i, j, 0));
                cellList.Add(new Vector2Int(i, j), NewCell);
            }
    }

    private void PutNewButtonIntoWall(Vector2Int position, ButtonCandy button)
    {
        Cell cell;
        if(cellList.TryGetValue(position, out cell))
        {
            cell.Button = button;
            cell.RefreshButtonPosition();
        }
    }

}