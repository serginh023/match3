using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Button Candy", menuName = "New Button Candy", order = 1 )]
public class ButtonCandyScriptable : ScriptableObject
{
    public Sprite icon;
    public string candyName;
}
