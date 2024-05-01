using UnityEngine;

//Este � um script em C# que controla o movimento de um jogador em um jogo Unity.

public class Player : MonoBehaviour
{
    // Refer�ncia para o componente Rigidbody2D do jogador
    [SerializeField]
    private new Rigidbody2D rigidbody;

    // Velocidade de movimento do jogador
    [SerializeField]
    public float velocidadeMovimento;

    // Posi��o de retorno na passagem
    public Vector3 posicaoDeRetorno;

    // Start � chamado antes da atualiza��o do primeiro quadro
    void Start()
    {
        // Restringir rota��o no eixo Z para evitar rota��o indesejada
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update � chamado uma vez por quadro
    void Update()
    {
        // Obt�m a entrada do teclado para movimento horizontal e vertical
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Cria um vetor de dire��o normalizado com base na entrada do teclado
        Vector2 direcao = new Vector2(horizontal, vertical).normalized;

        // Define a velocidade do jogador multiplicando a dire��o pela velocidade de movimento
        rigidbody.velocity = direcao * velocidadeMovimento;
    }

    // M�todo para retornar o jogador para uma posi��o espec�fica
    public void RetornarParaPosicaoDeRetorno()
    {
        // Define a posi��o do jogador como a posi��o de retorno
        transform.position = posicaoDeRetorno;
    }
}
