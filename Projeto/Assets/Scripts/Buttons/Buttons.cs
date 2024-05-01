using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script Unity parece ser um componente chamado "Buttons" que pode ser anexado a objetos no seu jogo.
//Ele controla o comportamento visual de um bot�o,
//permitindo alternar entre um estado selecionado e deselecionado.
public class Buttons : MonoBehaviour
{
    // Refer�ncia para o pr�prio GameObject deste bot�o
    [HideInInspector]
    public GameObject instance;

    // Sprite atual do bot�o
    [HideInInspector]
    public Sprite currentSprite;

    // Sprite original do bot�o
    [HideInInspector]
    public Sprite instanceSprite;

    // Sprite a ser usada quando o bot�o estiver desselecionado
    public Sprite buttonDeselected;

    // Sprite a ser usada quando o bot�o estiver selecionado
    public Sprite buttonSelected;

    // Indica se o bot�o est� selecionado ou n�o
    public bool selected;

    // Posi��o onde algo chamado "soul" ser� posicionado
    public Transform soulPosition;

    // M�todo chamado quando o script � inicializado
    void Awake()
    {
        // Define a refer�ncia instance como o pr�prio GameObject
        instance = this.gameObject;

        // Obt�m a sprite atual do bot�o e a armazena
        instanceSprite = instance.GetComponent<SpriteRenderer>().sprite;
        currentSprite = instanceSprite;
    }

    // M�todo chamado a cada quadro
    void Update()
    {
        // Atualiza a sprite do bot�o para ser igual � sprite atual
        instance.GetComponent<SpriteRenderer>().sprite = currentSprite;
    }
}
