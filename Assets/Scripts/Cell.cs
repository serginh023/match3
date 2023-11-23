using System;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private ButtonCandy button;
    public ButtonCandy Button 
    { 
        get => button; 
        set 
        {
            button = value;
            button.transform.SetParent(transform);
            RefreshButtonPosition();
        } 
    }
    private Vector2Int coord;

    public void SetPosition(Vector3 newPos)
    {
        transform.localPosition = newPos;
        coord = new Vector2Int((int)newPos.x, (int)newPos.y);
    }

    public void RefreshButtonPosition()
    {
        //button.transform.position = transform.position;
        button.transform.localPosition = Vector3.zero;
    }

    public void RemoveButton()
    {
        button = null;
    }

    public void VerifyClick(RaycastHit hit, Action<Cell> callback)
    {
        button.VerifyClick(hit, this, callback);
    }

    public void SetHover(bool hover)
    {
        button.Hover = hover;
    }

    public void SetCollider(bool enable)
    {
        button.SetCollider(enable);
    }
}