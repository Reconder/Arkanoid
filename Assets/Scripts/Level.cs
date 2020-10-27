using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] int blocks;
    SceneLoader sceneLoader;
    private void Start()
    {
        //blocks = 0;
        sceneLoader = FindObjectOfType<SceneLoader>();
    }
    // Start is called before the first frame update
    public void CountBreakableBlocks() { blocks++; }
    public void DestroyBlock()
    {
        blocks--;
        if (blocks<=0) { sceneLoader.LoadNextScene();}
    }
    // Update is called once per frame

}
