using UnityEngine;

public class BallControl : MonoBehaviour
{
    // Velocidade inicial da bola
    public float initialSpeed = 5f;

    // Aceleração após a colisão
    public float acceleration = 1.2f;

    // Variação máxima do ângulo após a colisão com o jogador
    public float maxAngleVariation = 15f;

    // Referência ao Rigidbody da bola
    private Rigidbody2D rb;

    // Flag para verificar se a bola está jogada
    private bool isBallLaunched = false;

    void Start()
    {
        // Obter o componente Rigidbody2D da bola
        rb = GetComponent<Rigidbody2D>();

        // Configurar a velocidade inicial da bola
        LaunchBall();
    }

    void Update()
    {
        // Verificar se a bola não foi lançada e se o jogador tocou na tela
        if (!isBallLaunched && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Lançar a bola
            LaunchBall();
        }
    }

    void LaunchBall()
    {
        // Gerar uma direção de lançamento aleatória
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), 1).normalized;

        // Aplicar a velocidade inicial à bola
        rb.velocity = randomDirection * initialSpeed;

        // A bola está agora lançada
        isBallLaunched = true;
    }

    // Função chamada quando a bola colide com algo
    void OnCollisionEnter2D(Collision2D other)
    {
        
            // Obter a posição de contato
            float contactPoint = other.contacts[0].point.x;

            // Calcular o novo ângulo de saída baseado na posição de contato
            float angle = (contactPoint - transform.position.x) * 5f;

            // Adicionar uma pequena aleatoriedade ao ângulo
            angle += Random.Range(-maxAngleVariation, maxAngleVariation);

            // Calcular a nova direção
            Vector2 direction = new Vector2(angle, 1).normalized;

            // Aplicar a nova direção à bola
            rb.velocity = direction * initialSpeed;

            // Aplicar aceleração
            rb.velocity *= acceleration;
        

        // Verificar se a bola colidiu com um bloco
        if (other.gameObject.CompareTag("Block"))
        {
            // Destruir o bloco
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("chao"))
        {
            rb.position = new Vector2(0, -2);
            rb.velocity = Vector2.zero; // Reset velocity when touching ground
        }
    }
}
