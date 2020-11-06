﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameStatus : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int blockScore = 5;
    [SerializeField] bool autoPlay;
    //[SerializeField] int Score;
    [SerializeField] TextMeshProUGUI scoreText;
    int Score;
    private void Awake()
    {
        SetUpSingleton();
        
    }
    private void Start()
    {
        scoreText.text = Score.ToString();
        SubscribeToBlocks();
    }

    private void SubscribeToBlocks()
    {
        Block[] blocks = FindObjectsOfType<Block>();
        foreach (Block block in blocks)
        {
            block.BlockDestroyed += OnBlockDestroyed;
        }
    }

    private void SetUpSingleton()
    {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);

        }
    }


    public void AddScore()
    {
        Score += blockScore;
        scoreText.text = Score.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }
    public void Reset()
    {
        Destroy(gameObject);
    }
    public bool AutoPlay() { return autoPlay; }

    public void OnBlockDestroyed(object block, EventArgs arg)
    {
        AddScore();
    }
}
