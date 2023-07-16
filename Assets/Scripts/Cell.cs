using UnityEngine;

public class Cell : MonoBehaviour
{
    private ButtonCandy button;

    public ButtonCandy Button { get => button; set => button = value; }

    public void SetPosition(Vector3 newPos)
    {
        transform.localPosition = newPos;
    }

    private void SetButtonPosition()
    {
        button.transform.localPosition = Vector3.zero;
    }
}
