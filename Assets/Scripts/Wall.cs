using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Wall : MonoBehaviour
{
    [SerializeField] private int rows;
    [SerializeField] private int collumns;
    [SerializeField] private Pool pool;
    [SerializeField] private Camera camera;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private List<ButtonCandyScriptable> scriptableList;
    private Dictionary<Vector2Int, Cell> cellList = new();
    private Coroutine current;
    private bool _anySelected = false;
    private Cell selected1;
    private Cell selected2;
    private Ray ray;

    private void Start()
    {
        PopulateWall();
        PopulateCells();
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

    public void VerifyRay()
    {
        ray = camera.ScreenPointToRay(Mouse.current.position.value);
        if (Physics.Raycast(ray, out var hit))
        {
            foreach (var pair in cellList)
            {
                var cell = pair.Value;
                cell.VerifyClick(hit, VerifySelectedCells);
            }
        }
    }

    private bool FindMatch()
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
                        return true;
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

    private void VerifySelectedCells(Cell cell)
    {
        if (!_anySelected)
        {
            selected1 = cell;
            _anySelected = true;
            selected1.SetHover(true);
        }
        else
        {
            selected2 = cell;
            if (!IsPossibleDistance(selected1, selected2))
            {
                _anySelected = false;
                RemoveSelectedVariables();
            }
            else
            {
                _anySelected = false;
                current = StartCoroutine(ChangeButtonsWithWait(ChangeButtons));
                //FindMatch();
            }
        }
    }

    private void ChangeButtons(Cell cell1, Cell cell2)
    {
        (cell1.Button, cell2.Button) = (cell2.Button, cell1.Button);
        cell1.RefreshButtonPosition();
        cell2.RefreshButtonPosition();
        ResetVariables();
    }

    private void ResetVariables()
    {
        selected1.SetHover(false);
        selected2.SetHover(false);
        selected1.SetCollider(true);
        selected2.SetCollider(true);
        RemoveSelectedVariables();
    }

    private bool IsPossibleDistance(Cell cell1, Cell cell2)
    {
        var distance = Vector3.Distance(cell1.transform.position, cell2.transform.position);
        return ( distance == 1);
    }

    private void RemoveSelectedVariables()
    {
        selected1.SetHover(false);
        selected2.SetHover(false);
        selected1 = default;
        selected2 = default;
    }

    private IEnumerator ChangeButtonsWithWait(Action<Cell, Cell> callback)
    {
        yield return new WaitForSeconds(.1f);
        callback?.Invoke(selected1, selected2);
    }
}