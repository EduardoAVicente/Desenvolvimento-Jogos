using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Projectile laserPrefab;
    public float speed = 5.0f;

    private bool laserActive;

    private void Update()
    {
        // Movimento do jogador com o mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        transform.position = Vector3.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);

        // Atirar com o clique do mouse
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!laserActive)
        {
            Projectile projectile = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            projectile.destroyed += LaserDestroyed;
            laserActive = true;
        }
    }

    private void LaserDestroyed()
    {
        laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Invader") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            // Condição de derrota
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void FixedUpdate()
    {
        // Limites da tela em coordenadas do mundo
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Tamanho do sprite do jogador
        Bounds playerBounds = GetComponent<SpriteRenderer>().bounds;

        // Calcula os offsets do sprite do jogador (metade do tamanho do sprite)
        float xOffset = playerBounds.size.x / 2;
        float yOffset = playerBounds.size.y / 2;

        // Posição atual do jogador
        Vector3 playerPosition = transform.position;

        // Limita a posição do jogador aos limites da tela, considerando os offsets do sprite
        playerPosition.x = Mathf.Clamp(playerPosition.x, -screenBounds.x + xOffset, screenBounds.x - xOffset);
        playerPosition.y = Mathf.Clamp(playerPosition.y, -screenBounds.y + yOffset, screenBounds.y - yOffset);

        // Atualiza a posição do jogador
        transform.position = playerPosition;
    }
}
