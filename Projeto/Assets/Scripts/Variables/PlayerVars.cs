using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

//Este script em C# é responsável por gerenciar as variáveis do jogador e implementar o sistema de dano e invencibilidade. 

public class PlayerVars : MonoBehaviour
{
    // Variáveis do jogador
    public float atkValue; // Valor do ataque
    public float defValue; // Valor da defesa
    public float health; // Vida do jogador

    // Cor para o efeito de piscar da alma
    [SerializeField] Color soulFlashing;
    public Color soulOriginal; // Cor original da alma
    float soulAlpha; // Valor de alfa da cor da alma
    float soulAlphaFlash; // Valor de alfa para o efeito de piscar
    float time; // Tempo restante para o fim do efeito de invencibilidade
    public float maxTime; // Tempo máximo de invencibilidade

    // Referência para o SpriteRenderer da alma
    [SerializeField] private SpriteRenderer soulSprite;

    bool invincible; // Indicador de invencibilidade

    // Instância estática para acesso global
    [HideInInspector]
    public static PlayerVars instance;

    // Chamado quando o objeto é criado
    void Awake() => instance = this;

    // Chamado no início
    void Start()
    {
        // Inicializa as variáveis de alfa da alma
        soulAlpha = soulSprite.color.a;
        soulAlphaFlash = soulAlpha / 2;

        // Armazena a cor original da alma
        soulOriginal = soulSprite.color;

        // Inicializa o tempo
        time = maxTime;
    }

    // Método para lidar com o dano recebido pelo jogador
    public void TakeDamage(float damageTaken)
    {
        if (!invincible)
        {
            // Reduz a vida do jogador pelo dano recebido
            health -= damageTaken;

            // Toca o som de dano
            AudioManager.instance.takingDamage();

            // Define o jogador como invencível temporariamente
            invincible = true;
        }
    }

    // Método para piscar a alma durante o período de invencibilidade
    void FlashSoul()
    {
        if (time > 0)
        {
            // Inverte o valor de alfa do efeito de piscar
            soulAlphaFlash = soulAlphaFlash * -1;

            // Aplica a cor de piscar à alma
            soulSprite.color = soulFlashing;
        }
        else
        {
            // Restaura a cor original da alma e redefine o tempo e a invencibilidade
            soulSprite.color = soulOriginal;
            time = maxTime;
            invincible = false;
        }
    }

    // Chamado a cada quadro
    void Update()
    {
        // Atualiza o tempo restante
        time -= Time.deltaTime;

        // Se o jogador estiver invencível, executa o efeito de piscar da alma
        if (invincible)
        {
            FlashSoul();
        }
    }
}
