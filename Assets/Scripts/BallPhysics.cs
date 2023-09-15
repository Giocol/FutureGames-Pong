using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = System.Numerics.Vector2;

public class BallPhysics : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject player1Score;
    [SerializeField] private GameObject player2Score;

    private int currentPlayer1Score;
    private int currentPlayer2Score;

    private Vector3 velocity;
    private new Rigidbody rigidbody;
    
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
        velocity = new Vector3(speed, -speed, 0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("goal"))
        {
            OnScoreGoal(other.gameObject);
        }
        
        ContactPoint contactPoint = other.GetContact(0);
        velocity = Vector3.Reflect(velocity.normalized, contactPoint.normal) *speed;

    }

    private void FixedUpdate()
    {
         rigidbody.velocity= velocity;
    }

    private void OnScoreGoal(GameObject goalGameObject)
    {
        if (goalGameObject.name.Equals("GoalPlayer1"))
        {
            Debug.Log("Player 2 scored!");
            currentPlayer2Score++;
            //player2Score.GetComponent<TextMeshProUGUI>().text = currentPlayer2Score.ToString();
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Player 1 scored!");
            currentPlayer1Score++;
            //player1Score.GetComponent<TextMeshProUGUI>().text = currentPlayer1Score.ToString();
            Destroy(this.gameObject);

        }
    }
}
