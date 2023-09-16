using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private int rows;
    [SerializeField] private int collumns;
    [SerializeField] private Pool pool;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private List<ButtonCandyScriptable> scriptableList;

    private Dictionary<Vector2Int, Cell> cellList = new();
    private Ray ray;

    private void Start()
    {
        PopulateWall();
        PopulateCells();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            foreach (var cell in cellList)
            {
                cell.Value.VerifyClick(hit);
            }
        }
    }

    private void PopulateWall()
    {
        for(var i = 0; i < rows; i++)
        {
            for(var j = 0; j < collumns; j++)
            {
                var newCell = Instantiate(cellPrefab, transform);
                newCell.SetPosition(new Vector3(i, j, 0));
                cellList.Add(new Vector2Int(i, j), newCell);
            }
        }
    }

    private void PutNewButtonIntoWall(Vector2Int position, ButtonCandy button)
    {
        Cell cell;
        if (!cellList.TryGetValue(position, out cell)) return;
        cell.Button = button;
        button.transform.SetParent(cell.transform);
        cell.RefreshButtonPosition();
    }

    private void PopulateCells()
    {
        ButtonCandyScriptable scriptable;
        for(var i = 0; i < rows; i++)
        {
             for(var j = 0; j < collumns; j++)
             {
                var go = pool.Get();
                var button = go.GetComponent<ButtonCandy>();
                scriptable = scriptableList[Random.Range(0, scriptableList.Count - 1)];
                button.SetIcon(scriptable.icon);
                button.name = scriptable.candyName;
                button.transform.name = scriptable.name;
                PutNewButtonIntoWall(new Vector2Int(i, j), button);
             }
        }
    }

    private void FindMatch()
    {
        var i = 0;
        var j = 0;
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
                        //TODO 1.Remove buttons (VFX too)
                        RemoveButton(cell);
                        //TODO 2.Add some score (VFX too)
                        //TODO 3.Call new buttons (Drop down spawn)
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
        
        //Verify vertical
        while (i < rows)
        {
            while (j < collumns)
            {
                pos = new Vector2Int(i, j);
                Cell cell;
                if (cellList.TryGetValue(pos, out cell))
                {
                    if (VerifyVertical(pos, cell.Button.name))
                    {
                        //Done good!
                        //TODO 1.Remove buttons (VFX too)
                        RemoveButton(cell);
                        //TODO 2.Add some score (VFX too)
                        //TODO 3.Call new buttons (Drop down spawn)
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
        var offset = new Vector2Int(1, 0);
        var pos1 = pos + offset;
        var pos2 = pos1 + offset;
        if (cellList.TryGetValue(pos1, out var aux))
        {
            if (cellList.TryGetValue(pos2, out var aux2))
            {
                if (aux.Button.name == name && aux2.Button.name == name)
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
    
    private bool VerifyVertical(Vector2Int pos, string name)
    {
        var offset = new Vector2Int(0, 1);
        var pos1 = pos + offset;
        var pos2 = pos1 + offset;
        
        if (cellList.TryGetValue(pos1, out var aux))
        {
            if (cellList.TryGetValue(pos2, out var aux2))
            {
                if (aux.Button.name == name && aux2.Button.name == name)
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

    private void RemoveButton(Cell cell)
    {
        // pool.Retrieve(cell.Button.gameObject);
        // cell.RemoveButton();
    }
    
}