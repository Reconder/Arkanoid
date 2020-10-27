using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //config params
    [SerializeField] AudioClip breakClip;
    [SerializeField] GameObject destoryVFX;

    [SerializeField] Sprite[] hitSprites;
 
    Level level;
    GameStatus gameStatus;
    SpriteRenderer spriteRenderer;


    [SerializeField] int hits; 
    void Start()
    {

        level = FindObjectOfType<Level>();
        gameStatus = FindObjectOfType<GameStatus>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (tag == "Breakable")
        {
            
            
            level.CountBreakableBlocks();
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {

            HandleHit();

        }


    }

    private void HandleHit()
    {
        TriggerDestroySFX();
        hits++;
        int maxHits = hitSprites.Length + 1;
        if (hits >= maxHits) { DestroyBlock(); }
        else { ShowNextHitSprite(); }
    }

    private void ShowNextHitSprite()
    {
        if (hitSprites[hits - 1] != null)
        { spriteRenderer.sprite = hitSprites[hits - 1]; }
        else { Debug.Log("Missing a damaged block sprite: "  + gameObject.name); }
    }

    private void DestroyBlock()
    {
        
        TriggerDestroyVFX();
        Destroy(gameObject);
        level.DestroyBlock();
        gameStatus.AddScore();
    }

    private void TriggerDestroySFX()
    {
        AudioSource.PlayClipAtPoint(breakClip, Camera.main.transform.position);
    }

    private void TriggerDestroyVFX()
    {
        GameObject destroy = Instantiate(destoryVFX, transform.position, transform.rotation);
        Destroy(destroy, 2f);
    }
}
