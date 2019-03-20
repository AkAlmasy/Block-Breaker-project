using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    // config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockDestroyEffects;
    [SerializeField] Sprite[] hitSprites;

    // cached reference
    Level level;


    // state variables
    [SerializeField] int timesHit;

    private void Start()
    {
        CountBreakableBlocks();

    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
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
        timesHit++;
        int maxHits = hitSprites.Length;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array" + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        Destroy(gameObject);
        level.BlockDestroyed();
        FindObjectOfType<GameStatus>().AddToScore();
        TriggerBlockDestroyEffect();
    }

    private void TriggerBlockDestroyEffect()
    {
        GameObject destroyEffect = Instantiate(blockDestroyEffects,transform.position, transform.rotation);
        Destroy(destroyEffect, 2f);
    }

}
