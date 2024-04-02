using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 direction = Vector3.up; // Direção do movimento do projétil
    public bool isPlayerProjectile = false; // Verifica se o projétil é do jogador

    // Update is called once per frame
    void Update()
    {
        // Movimenta o projétil na direção especificada
        transform.Translate(direction * speed * Time.deltaTime);

        // Verifica se o projétil está fora da tela e o destrói se estiver
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }

    // Função para lidar com colisões
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o projétil atingiu um objeto que não é o jogador (para evitar colidir consigo mesmo)
        if (isPlayerProjectile && !collision.CompareTag("Player"))
        {
            // Destroi o projétil ao colidir com qualquer outro objeto que não seja o jogador
            Destroy(gameObject);
        }
        else if (!isPlayerProjectile && collision.CompareTag("Player"))
        {
            // Destroi o projétil do inimigo ao colidir com o jogador
            Destroy(gameObject);
        }
    }
}
