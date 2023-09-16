using UnityEngine;

public class Cell : MonoBehaviour
{
    private ButtonCandy button;
    public ButtonCandy Button { get => button; set => button = value; }
    private Vector2Int coord;

    public void SetPosition(Vector3 newPos)
    {
        transform.localPosition = newPos;
        coord = new Vector2Int((int)newPos.x, (int)newPos.y);
    }

    public void RefreshButtonPosition()
    {
        button.transform.localPosition = Vector3.zero;
    }

    public void RemoveButton()
    {
        button = null;
    }

    public void VerifyClick(RaycastHit hit)
    {
        button.VerifyClick(hit);
    }
}