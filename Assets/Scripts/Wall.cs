using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private int collumns;
    [SerializeField] private int rows;
    [SerializeField] private Cell cell;
    [SerializeField] private Dictionary<Vector2Int, Cell> cellList = new Dictionary<Vector2Int, Cell>();
    [SerializeField] private List<ButtonCandyScriptable> buttonCandyScriptableList;
    [SerializeField] private Sprite testeSprite;
    [SerializeField] private Pool pool;

    private void Start()
    {   
        PopulateWall();
        PopulateCells();
    }

    private void PopulateWall()
    {
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < collumns; j++)
            {
                Cell NewCell = Instantiate(cell);
                NewCell.transform.SetParent(transform);
                NewCell.SetPosition(new Vector3(i, j, 0));
                cellList.Add(new Vector2Int(i, j), NewCell);
            }
        }
    }

    private void PutNewButtonIntoWall(Vector2Int position, ButtonCandy button)
    {
        Cell cell;
        if(cellList.TryGetValue(position, out cell))
        {
            cell.Button = button;
            button.transform.SetParent(cell.transform);
            cell.RefreshButtonPosition();
        }
    }

    private void PopulateCells()
    {
        int scriptable = 0;
        ButtonCandyScriptable buttonCandyScriptable;
        for(int i = 0; i < rows; i++)
        {
             for(int j = 0; j < collumns; j++)
             {
                var go = pool.Get();
                var bc = go.GetComponent<ButtonCandy>();
                buttonCandyScriptable = buttonCandyScriptableList[Random.Range(0, buttonCandyScriptableList.Count - 1)];
                bc.SetIcon(buttonCandyScriptable.icon);
                bc.name = buttonCandyScriptable.name;
                PutNewButtonIntoWall(new Vector2Int(i, j), bc);
             }
        }
    }
    

}