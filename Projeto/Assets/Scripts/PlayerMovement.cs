using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este � um script em C# para controlar o movimento do jogador em um jogo Unity.
public class PlayerMovement : MonoBehaviour
{
    // Velocidade do jogador
    public float speed;

    // Vari�veis para armazenar o movimento horizontal e vertical
    float xMovement;
    float yMovement;

    // Refer�ncia para o componente Rigidbody2D do jogador
    Rigidbody2D rb;

    void Start()
    {
        // Obt�m a refer�ncia para o componente Rigidbody2D do jogador
        rb = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Obt�m a entrada do teclado para o movimento horizontal e vertical
        xMovement = Input.GetAxisRaw("Horizontal");
        yMovement = Input.GetAxisRaw("Vertical");

        // Define a velocidade do jogador multiplicando a entrada de movimento pelo valor de velocidade
        rb.velocity = new Vector2(xMovement * speed, yMovement * speed);
    }
}
