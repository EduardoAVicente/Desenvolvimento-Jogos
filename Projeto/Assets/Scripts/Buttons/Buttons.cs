using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script Unity parece ser um componente chamado "Buttons" que pode ser anexado a objetos no seu jogo.
//Ele controla o comportamento visual de um botão,
//permitindo alternar entre um estado selecionado e deselecionado.
public class Buttons : MonoBehaviour
{
    // Referência para o próprio GameObject deste botão
    [HideInInspector]
    public GameObject instance;

    // Sprite atual do botão
    [HideInInspector]
    public Sprite currentSprite;

    // Sprite original do botão
    [HideInInspector]
    public Sprite instanceSprite;

    // Sprite a ser usada quando o botão estiver desselecionado
    public Sprite buttonDeselected;

    // Sprite a ser usada quando o botão estiver selecionado
    public Sprite buttonSelected;

    // Indica se o botão está selecionado ou não
    public bool selected;

    // Posição onde algo chamado "soul" será posicionado
    public Transform soulPosition;

    // Método chamado quando o script é inicializado
    void Awake()
    {
        // Define a referência instance como o próprio GameObject
        instance = this.gameObject;

        // Obtém a sprite atual do botão e a armazena
        instanceSprite = instance.GetComponent<SpriteRenderer>().sprite;
        currentSprite = instanceSprite;
    }

    // Método chamado a cada quadro
    void Update()
    {
        // Atualiza a sprite do botão para ser igual à sprite atual
        instance.GetComponent<SpriteRenderer>().sprite = currentSprite;
    }
}
