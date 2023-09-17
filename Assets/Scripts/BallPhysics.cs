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

    private Vector3 velocity;
    private new Rigidbody rigidbody;
    
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
        velocity = new Vector3(speed, speed, 0);
    }

    private void OnCollisionEnter(Collision other)
    {
        ContactPoint contactPoint = other.GetContact(0);
        velocity = Vector3.Reflect(velocity.normalized, contactPoint.normal) * speed;
        Debug.Log(velocity);
    }

    private void FixedUpdate()
    {
         rigidbody.velocity= velocity;
    }
}
