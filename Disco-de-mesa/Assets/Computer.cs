using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public float speed = 10f; // Velocidade base
    public float boundY = 2f;
    public float minX = 0.59f; // Limite mínimo de x
    public float maxX = 5.79f; // Limite máximo de x
    public float difficultyIncrease = 0.2f; // Aumento de dificuldade por acerto na bola
    private float currentSpeed; // Rastreia a velocidade atual com o aumento de dificuldade
    private Rigidbody2D rb2d;
    private Vector2 targetPosition = new Vector2(4f, 0f); // Posição alvo

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentSpeed = speed; // Inicializa a velocidade atual para a velocidade base
    }

    void Update()
    {
        // Rastreia a posição vertical da bola
        Vector2 ballPosition = GameObject.FindGameObjectWithTag("Ball").transform.position;

        // Verifica se a posição x da bola é menor que 0
        if (ballPosition.x < 0)
        {
            // Se for, mova gradualmente o objeto para a posição desejada
            transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
            return; // Interrompe o método Update para evitar que o resto do código seja executado
        }

        // Move a raquete em direção à posição prevista da bola
        var targetY = Mathf.Lerp(transform.position.y, ballPosition.y, 0.4f); // Ajusta a suavidade da previsão
        var targetX = Mathf.Lerp(transform.position.x, ballPosition.x, 0.4f); // Ajusta a suavidade da previsão
        var vel = rb2d.velocity;
        vel.y = (targetY - transform.position.y) * currentSpeed;
        vel.x = (targetX - transform.position.x) * currentSpeed;
        
        // Enforce limites de movimento
        var pos = transform.position;

        if (pos.y > boundY)
        {
            pos.y = boundY;
        }
        else if (pos.y < -boundY)
        {
            pos.y = -boundY;
        }

        // Limita a posição x entre minX e maxX
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        transform.position = pos;

        rb2d.velocity = vel;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Aumenta a dificuldade ao acertar na bola
            currentSpeed += difficultyIncrease;
        }
    }
}
