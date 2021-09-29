using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class RoofOcclusion : MonoBehaviour
{
    private Collider2D col;
    private SpriteRenderer sprite;
    public bool occluded = false;
    public float transitionSpeed = 0.0f;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();

        if (!col.isTrigger)
        {
            col.isTrigger = true;
        }
    }

    private void Update()
    {
        if (occluded && sprite.color.a > 0)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - transitionSpeed * Time.deltaTime);
        }
        else if (!occluded && sprite.color.a < 1)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + transitionSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            occluded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            occluded = false;
        }
    }
}
