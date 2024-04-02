using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    public float speed = 5f;
    public Projectile laserPrefab;

    private Projectile laser;

    private void Update()
    {
        Vector3 position = transform.position;

        // Update the position of the player based on the input
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            position.y += speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            position.y -= speed * Time.deltaTime;
        }

        // Clamp the position of the character so they do not go out of bounds
        Vector3 bottomEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 topEdge = Camera.main.ViewportToWorldPoint(Vector3.up);
        position.y = Mathf.Clamp(position.y, bottomEdge.y, topEdge.y);

        // Set the new position
        transform.position = position;

        // Only one laser can be active at a given time so first check that
        // there is not already an active laser
        if (laser == null && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Missile") ||
            other.CompareTag("Invader"))
        {
            GameManager.Instance.OnPlayerKilled(this);
        }
    }
}
