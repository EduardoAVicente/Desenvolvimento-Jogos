using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControladorCamera : MonoBehaviour
{

    /// <summary>
    /// Transform do jogador que será seguido pela câmera
    /// </summary>
    [SerializeField]
    private Transform jogador;

    /// <summary>
    /// TRUE se a câmera deve seguir o jogador no
    /// eixo X (para os lados) e FALSE caso contrário
    /// </summary>
    [SerializeField]
    private bool moverX;

    /// <summary>
    /// TRUE se a câmera deve seguir o jogador no
    /// eixo Y (para cima e para baixo) e FALSE caso contrário
    /// </summary>
    [SerializeField]
    private bool moverY;

    /// <summary>
    /// Velocidade de movimentação da câmera para seguir o jogador
    /// </summary>
    [SerializeField]
    private float velocidadeMovimentacao;

    

    private void FixedUpdate()
    {
        // Posição alvo da movimentação (para onde a câmera deve mover-se)
        Vector3 posicaoAlvo = this.jogador.position;

        if (!this.moverX)
        {
            posicaoAlvo.x = this.transform.position.x;
        }

        if (!this.moverY)
        {
            posicaoAlvo.y = this.transform.position.y;
        }



        // Calcula a posição final da câmera
        // enquanto ela se move em direção ao jogador,
        // utilizando a velocidade de movimentação definida anteriormente                        
        Vector3 posicaoFinal = Vector3.Lerp(
            this.transform.position, // Posição de origem
            posicaoAlvo, // Posição de destino
            (this.velocidadeMovimentacao * Time.deltaTime) // Passo
        );

        // Mantém a câmera na mesma posição Z, para continuar
        // visualizando os objetos do jogo
        posicaoFinal.z = this.transform.position.z;
        // Move a câmera para a nova posição
        this.transform.position = posicaoFinal;
    }
}