using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    [SerializeField] private bool blueBlock;
    [SerializeField] private Sprite startBlock;
    [SerializeField] private Sprite endBlock;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private Lever lever;

    private bool isFull = true;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        lever = FindObjectOfType<Lever>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = lever.IsLeverActive() ? endBlock : startBlock;
        if (blueBlock)
        {
            boxCollider2D.enabled = !lever.IsLeverActive();
        }
        else
        {
            boxCollider2D.enabled = lever.IsLeverActive();
        }
    }
}
