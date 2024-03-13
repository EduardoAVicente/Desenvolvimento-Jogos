/* 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour 
{
    public float speed = 10;
    public float boundY = 2.25f;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Track the ball's vertical position
        Vector2 ballPosition = GameObject.FindGameObjectWithTag("Ball").transform.position;

        // Move the racquet towards the ball's predicted position
        var targetY = Mathf.Lerp(transform.position.y, ballPosition.y, 0.4f); // Adjust prediction smoothness
        var vel = rb2d.velocity;
        vel.y = (targetY - transform.position.y) * speed;

        // Enforce movement bounds
        var pos = transform.position;
        if (pos.y > boundY)
        {
            pos.y = boundY;
        }
        else if (pos.y < -boundY)
        {
            pos.y = -boundY;
        }
        transform.position = pos;

        rb2d.velocity = vel;
    }
}

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public float speed = 10f; // Base speed
    public float boundY = 2f;
    public float difficultyIncrease = 0.2f; // Increase per ball hit
    private float currentSpeed; // Tracks current speed with difficulty increase
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentSpeed = speed; // Initialize current speed to base speed
    }

    void Update()
    {
        // Track the ball's vertical position
        Vector2 ballPosition = GameObject.FindGameObjectWithTag("Ball").transform.position;

        // Move the racquet towards the ball's predicted position
        var targetY = Mathf.Lerp(transform.position.y, ballPosition.y, 0.4f); // Adjust prediction smoothness
        var vel = rb2d.velocity;
        vel.y = (targetY - transform.position.y) * currentSpeed;

        // Enforce movement bounds
        var pos = transform.position;
        if (pos.y > boundY)
        {
            pos.y = boundY;
        }
        else if (pos.y < -boundY)
        {
            pos.y = -boundY;
        }
        transform.position = pos;

        rb2d.velocity = vel;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Increase difficulty on ball hit
            currentSpeed += difficultyIncrease;
        }
    }
}