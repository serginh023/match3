using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class ButtonCandy : MonoBehaviour
{
    [SerializeField] private Sprite icon;
    [SerializeField] private AudioClip clip;
    [SerializeField] private SpriteRenderer hovering;
    [SerializeField] private BoxCollider coll;
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private AudioSource audioSource;
    
    public string Name => icon.name;

    public bool Hover
    {
        set
        {
            hovering.enabled = value;
        }
    }

    public void VerifyClick(RaycastHit hit, Cell cell, Action<Cell> callback)
    {
        if (hit.collider != coll) return;
        coll.enabled = false;
        callback?.Invoke(cell);
        Debug.Log("Clicado" + icon.name);
        // PlayAudio();
    }
    
    public void SetIcon(Sprite newSprite)
    {
        icon = newSprite;
        rend.sprite = icon;
        FitColliderToImage();
    }

    public void SetAudio(AudioClip audioClip)
    {
        clip = audioClip;
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

    private void FitColliderToImage()
    {
         coll.size = new Vector2(icon.bounds.size.x, icon.bounds.size.y);
    }

    public void SetCollider(bool enable)
    {
        coll.enabled = enable;
    }
}