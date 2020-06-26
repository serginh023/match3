using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFlying : MonoBehaviour
{
    private Object[] textures;
    private float time = 6f;
    public Vector3 direction;


    private void Start()
    {
        textures = Resources.LoadAll("Sprites/Gems", typeof(Sprite));

        Sprite textura = (Sprite) textures[Random.Range(0, textures.Length)];
        GetComponent<SpriteRenderer>().sprite = textura;

        direction /= 45f;
    }

    void Update()
    {
        transform.Translate(direction);

        time -= Time.deltaTime;

        if(time <= 0)
        {
            Destroy(gameObject);
        }
    }
}
