using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] int blocks;
    SceneLoader sceneLoader;
    private void Awake()
    {
        //blocks = 0;
        sceneLoader = FindObjectOfType<SceneLoader>();
        SubscribeToBlocks();
    }
    public void CountBreakableBlocks(object block, EventArgs args) { blocks++; }

    private void SubscribeToBlocks()
    {
        Block[] blocks = FindObjectsOfType<Block>();
        foreach (Block block in blocks)
        {
            block.BlockDestroyed += OnBlockDestroyed;
            block.CountBlocks += CountBreakableBlocks;
        }
    }
    public void OnBlockDestroyed(object block, EventArgs args)
    {
        blocks--;
        if (blocks<=0) { sceneLoader.LoadNextScene();}
    }
    // Update is called once per frame

}
