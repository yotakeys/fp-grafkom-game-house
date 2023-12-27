using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedScript : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private int frameCounter = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Invoke(nameof(Animate), 0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        frameCounter++;

        if (frameCounter >= sprites.Length)
        {
            frameCounter = 0;
        }

        if (frameCounter >= 0 && frameCounter < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frameCounter];
        }

        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    }
}
