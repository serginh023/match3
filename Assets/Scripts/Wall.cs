using System.Collections.Generic;
using UnityEngine;

enum Direction
{
    VERTICAL,
    HORIZONTAL
}
public class Wall : MonoBehaviour
{
    [SerializeField] private int collumns;
    [SerializeField] private int rows;
    [SerializeField] private Cell cellPrefab;
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
                Cell NewCell = Instantiate(cellPrefab);
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

    private void FindMatch()
    {
        int i = 0;
        int j = 0;
        var pos = new Vector2Int();
        
        //Verify horizontal
        while (i < rows)
        {
            while (j < collumns)
            {
                pos = new Vector2Int(i, j);
                Cell cell;
                if (cellList.TryGetValue(pos, out cell))
                {
                    if (VerifyHorizontal(pos, cell.Button.name))
                    {
                        //Done good!
                        //1.Remove buttons (VFX too)
                        //2.Add some score (VFX too)
                        //3.Call new buttons (Drop down spawn)
                        i = 0;
                        j = 0;
                    }
                    else
                    {
                        j++;
                    }
                }
            }
            i++;
        }
    }

    private bool VerifyHorizontal(Vector2Int pos, string name)
    {
        Cell aux;
        Cell aux2;
        var pos1 = pos + new Vector2Int(1, 0);
        var pos2 = pos1 + new Vector2Int(1, 0);
        if (cellList.TryGetValue(pos1, out aux))
        {
            if (cellList.TryGetValue(pos2, out aux2))
            {
                if (aux.Button.name == name
                    && aux2.Button.name == name)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        
        

        return false;
    }


}