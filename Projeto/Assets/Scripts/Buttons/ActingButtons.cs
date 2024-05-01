using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script em C# é responsável por controlar o comportamento dos botões de ação durante uma batalha no jogo Unity.

public class ActingButtons : MonoBehaviour
{
    // Referência para a instância do botão
    [HideInInspector]
    public GameObject instance;

    // Indicador se o botão está selecionado
    [HideInInspector]
    public bool selected;

    // Posição da alma associada a este botão
    public Transform soulPosition;

    // Variáveis de ação associadas a este botão
    [HideInInspector]
    public ActVars actVars;

    // Chamado quando o objeto é criado
    void Awake()
    {
        // Obtém a referência para a instância do botão
        instance = this.gameObject;

        // Obtém as variáveis de ação associadas a este botão
        actVars = this.GetComponent<ActVars>();
    }
}
