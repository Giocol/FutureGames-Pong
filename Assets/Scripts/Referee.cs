using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Referee : MonoBehaviour
{
    

    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private TMP_Text player1ScoreText;
    [SerializeField] private TMP_Text player2ScoreText;
    [SerializeField] private TMP_Text winText;

    [SerializeField] private int timeBeforeNewGame = 3;
    [SerializeField] private int scoreToWin = 5;


    private int player1Score;
    private int player2Score;
    private GameObject ball;

    // Instead of doing this, I'd ideally make the Referee class an observer and make BallPhysics a subject
    void Update()
    { 
        if (ball)
        {
            if(ball.transform.position.x > player1.transform.position.x + 2)
            {
                Destroy(ball);
                player2Score++;
                Debug.Log(player2Score);
                player2ScoreText.text = player2Score.ToString(); 
            } 
            else if (ball.transform.position.x < player2.transform.position.x - 2)
            {
                Destroy(ball);
                player1Score++;
                player1ScoreText.text = player1Score.ToString();
            }
        }
        else if(player1.GetComponent<PlayerInput>())
        {
            ball = player1.GetComponent<PlayerInput>().BallInstance; // fetch the ball instance from the PlayerInput class, might be a bit too hacky
        }
        
        if(player1Score >= scoreToWin || player2Score >= scoreToWin)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    private IEnumerator HandleWinCondition()
    {
        if (player1Score >= scoreToWin)
        {
            winText.text = "Player1 wins!";
        }
        else if (player2Score >= scoreToWin)
        {
            winText.text = "Player2 wins!";
        }

        player1Score = 0;
        player2Score = 0;

        yield return new WaitForSeconds(timeBeforeNewGame);
        Debug.Log("resetting");
        player1ScoreText.text = player1Score.ToString();
        player2ScoreText.text = player2Score.ToString();
        winText.text = "";
    }
}
