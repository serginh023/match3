using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public delegate void OnClickHandler(Piece piece);
    public static event OnClickHandler ClickEvent;

    private void Move(Vector3 direction)
    {
        transform.position += direction;
    }

    public void MoveLeft()
    {
        Move(new Vector3(-1, 0, 0));
    }

    public void MoveRight()
    {
        Move(new Vector3(1, 0, 0));
    }

    public void MoveUp()
    {
        Move(new Vector3(0, 1, 0));
    }

    public void MoveDown()
    {
        Move(new Vector3(0, -1, 0));
    }


    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = new Color(.9f, .9f, .9f, .5f);

        if (ClickEvent != null)

            ClickEvent(this);
    }
}
