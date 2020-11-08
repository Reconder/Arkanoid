using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockEventArgs: EventArgs
{
    public int blockScore { get; set; }
}

//Breakable
public class Block : MonoBehaviour
{
    //config params
    [SerializeField] AudioClip breakClip;
    [SerializeField] GameObject destoryVFX;
    [SerializeField] int blockScore;
    [SerializeField] Sprite[] hitSprites;
 
    SpriteRenderer spriteRenderer;


    public delegate void BlockDestroyedEventHandler(object source, BlockEventArgs args);

    public event BlockDestroyedEventHandler BlockDestroyed;

    public delegate void CountBlocksEventHandler(object source, EventArgs args);

    public event CountBlocksEventHandler CountBlocks;

    [SerializeField] int hits; 
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        CountBlocks?.Invoke(this, EventArgs.Empty);


    }
    private void OnCollisionEnter2D(Collision2D collision) => HandleHit();

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
        OnBlockDestroyed(blockScore);
    }

    private void OnBlockDestroyed(int score) => BlockDestroyed?.Invoke(this, new BlockEventArgs() { blockScore = score });

    private void TriggerDestroySFX() => AudioSource.PlayClipAtPoint(breakClip, Camera.main.transform.position);

    private void TriggerDestroyVFX()
    {
        GameObject destroy = Instantiate(destoryVFX, transform.position, transform.rotation);
        Destroy(destroy, 2f);
    }
}
