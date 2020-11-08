using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    [SerializeField] float cameraSize = 16f;
    Vector2 paddlePos;
    GameStatus gameStatus;
    Ball ball;


    private void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        ball = FindObjectOfType<Ball>();    
    }
    void Update() => PaddleMov();

    private void PaddleMov()
    {
        float newX;
        if (gameStatus.AutoPlay) { newX = ball.transform.position.x; }
        else { newX = Mathf.Clamp(Input.mousePosition.x / Screen.width * cameraSize, 1f, cameraSize - 1f); }
        paddlePos = new Vector2(newX, transform.position.y);
        transform.position = paddlePos;

    }
    public Vector2 GetPos() => transform.position;
}
