using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script em C# � respons�vel por controlar o comportamento dos bot�es de a��o durante uma batalha no jogo Unity.

public class ActingButtons : MonoBehaviour
{
    // Refer�ncia para a inst�ncia do bot�o
    [HideInInspector]
    public GameObject instance;

    // Indicador se o bot�o est� selecionado
    [HideInInspector]
    public bool selected;

    // Posi��o da alma associada a este bot�o
    public Transform soulPosition;

    // Vari�veis de a��o associadas a este bot�o
    [HideInInspector]
    public ActVars actVars;

    // Chamado quando o objeto � criado
    void Awake()
    {
        // Obt�m a refer�ncia para a inst�ncia do bot�o
        instance = this.gameObject;

        // Obt�m as vari�veis de a��o associadas a este bot�o
        actVars = this.GetComponent<ActVars>();
    }
}
