using System;
using System.Collections;
using UnityEngine;

public class ButtonCandy : MonoBehaviour
{
    [SerializeField] private Sprite icon;
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject hover;
    [SerializeField] private Collider collider;
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private AudioSource audioSource;
    
    public string Name => icon.name;

    private void VerifyClick(RaycastHit hit, Action callback)
    {
        if (hit.collider != collider) return;
        ToogleHover();
        PlayAudio();
        callback.Invoke();
    }
    
    public void SetIcon(Sprite newSprite)
    {
        icon = newSprite;
        rend.sprite = icon;
    }

    public void SetAudio(AudioClip clip)
    {
        this.clip = clip;
    }
    
    public void PlayAudio()
    {
        audioSource.clip = clip;
        audioSource.Play();
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
    
    private void ToogleHover()
    {
        var status = hover.activeInHierarchy;
        hover.SetActive(!status);
    }
}