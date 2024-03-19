using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed = 30.0f;
    private Transform racket;
    private GameObject player;

    

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Invoke("GoBall", 2);
    }


    void GoBall()
    {
        float rand = Random.Range(0, 2);
        if (rand < 1)
        {
            rb2d.AddForce(new Vector2(20, -15));
        }
        else
        {
            rb2d.AddForce(new Vector2(-20, -15));
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().player;

        if (coll.gameObject.CompareTag("chao"))
        {
            rb2d.position = new Vector2(0, -2);
            rb2d.velocity = Vector2.zero; // Reset velocity when touching ground
        }else {
            Vector2 vel = rb2d.velocity;
            vel.x = rb2d.velocity.x;
            vel.y = (rb2d.velocity.y / 2) + (coll.collider.attachedRigidbody.velocity.y / 3) + speed;
            rb2d.velocity = vel;
          }
        

    }

        
    void ResetBall()
    {
        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    void RestartGame()
    {
        ResetBall();
        Invoke("GoBall", 1);
    }

    public void AddBall()
    {
        Invoke("GoBall", 1);
    }

}