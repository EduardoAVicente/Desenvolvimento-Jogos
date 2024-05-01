using UnityEngine;

//Este é um script em C# que controla o movimento de um jogador em um jogo Unity.

public class Player : MonoBehaviour
{
    // Referência para o componente Rigidbody2D do jogador
    [SerializeField]
    private new Rigidbody2D rigidbody;

    // Velocidade de movimento do jogador
    [SerializeField]
    public float velocidadeMovimento;

    // Posição de retorno na passagem
    public Vector3 posicaoDeRetorno;

    // Start é chamado antes da atualização do primeiro quadro
    void Start()
    {
        // Restringir rotação no eixo Z para evitar rotação indesejada
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update é chamado uma vez por quadro
    void Update()
    {
        // Obtém a entrada do teclado para movimento horizontal e vertical
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Cria um vetor de direção normalizado com base na entrada do teclado
        Vector2 direcao = new Vector2(horizontal, vertical).normalized;

        // Define a velocidade do jogador multiplicando a direção pela velocidade de movimento
        rigidbody.velocity = direcao * velocidadeMovimento;
    }

    // Método para retornar o jogador para uma posição específica
    public void RetornarParaPosicaoDeRetorno()
    {
        // Define a posição do jogador como a posição de retorno
        transform.position = posicaoDeRetorno;
    }
}
