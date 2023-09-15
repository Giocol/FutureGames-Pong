using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float fieldBounds;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Vector3 ballStartPosition;
    
    private InputAction movementAction;
    private Controls controls;
    
    private void Awake()
    {
        controls = new Controls();
        
        switch (gameObject.name)
        {
            // This is a really bad, hacky solution Sebastian forgive me lol
            case "Player1":
                movementAction = controls.Player.Player1Movement;
                controls.Player.SpawnBall.performed += _ => SpawnBall();
                break;
            case "Player2":
                movementAction = controls.Player.Player2Movement;
                break;
            default:
                Debug.LogError("Not a player!");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {            
        Vector2 moveDir = movementAction.ReadValue<Vector2>();
        
        if (transform.position.y < fieldBounds && moveDir.y > 0) { // Move up bounds check
            transform.Translate(new Vector3(0, moveDir.y, 0) * speed * Time.deltaTime);
        } else if (transform.position.y > -fieldBounds && moveDir.y < 0) { // Move down bounds check
            transform.Translate(new Vector3(0, moveDir.y, 0) * speed * Time.deltaTime);
        }
    }

    private void SpawnBall()
    {
        if (!GameObject.FindObjectOfType<BallPhysics>())
        {
            Instantiate(ballPrefab, ballStartPosition, Quaternion.identity);
        }
    }
    
    void OnEnable()
    {
        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }
}
