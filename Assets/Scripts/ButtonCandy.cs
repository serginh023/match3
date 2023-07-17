using System;
using System.Collections;
using UnityEngine;

public class ButtonCandy : MonoBehaviour
{
    [SerializeField] private Sprite icon;
    [SerializeField] private AudioClip audio;
    [SerializeField] private GameObject hover;
    [SerializeField] private SpriteRenderer renderer;

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
        renderer.sprite = icon;
    }

    public void SetAudio(AudioClip clip)
    {
        audio = clip;
    }

    public void NavigateToPosition(Vector2 newPosition)
    {
        StartCoroutine(NavigationToPosition(newPosition, 1f, null));
    }

    private IEnumerator NavigationToPosition(Vector2 targetPosition, float duration, Action callback)
    {
        float time = 0f;
        Vector2 start = transform.position;
        while(time < duration)
        {
            transform.position = Vector3.Lerp(start, targetPosition, time / duration);
            time += Time.deltaTime;
        }

        yield return null;

        callback?.Invoke();
    }
}