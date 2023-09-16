using System;
using System.Collections;
using UnityEngine;

public class ButtonCandy : MonoBehaviour
{
    [SerializeField] private Sprite icon;
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject hover;
    [SerializeField] private BoxCollider coll;
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private AudioSource audioSource;
    
    public string Name => icon.name;

    public void VerifyClick(RaycastHit hit)
    {
        if (hit.collider != coll) return;
        // ToogleHover();
        PlayAudio();
    }
    
    public void SetIcon(Sprite newSprite)
    {
        icon = newSprite;
        rend.sprite = icon;
        FitColliderToImage();
    }

    public void SetAudio(AudioClip audioClip)
    {
        this.clip = audioClip;
    }
    
    private void PlayAudio()
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
        var time = 0f;
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

    private void FitColliderToImage()
    {
         coll.size = new Vector3(icon.bounds.size.x, icon.bounds.size.y);
         
    }
}