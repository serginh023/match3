using UnityEngine;

public class ButtonCandy : MonoBehaviour
{
    [SerializeField] private Sprite icon;
    [SerializeField] private AudioClip audio;
    [SerializeField] private GameObject hover;

    private void OnClick()
    {
        /*
        play audio
        enable hover
        */
    }

    public void SetIcon(Sprite newSprite)
    {
        icon = newSprite;
    }

    public void SetAudio(AudioClip clip)
    {
        audio = clip;
    }

    public void NavigateToPosition(Vector3 newPosition)
    {
        
    }
}