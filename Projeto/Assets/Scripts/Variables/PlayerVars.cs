using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

//Este script em C# � respons�vel por gerenciar as vari�veis do jogador e implementar o sistema de dano e invencibilidade. 

public class PlayerVars : MonoBehaviour
{
    // Vari�veis do jogador
    public float atkValue; // Valor do ataque
    public float defValue; // Valor da defesa
    public float health; // Vida do jogador

    // Cor para o efeito de piscar da alma
    [SerializeField] Color soulFlashing;
    public Color soulOriginal; // Cor original da alma
    float soulAlpha; // Valor de alfa da cor da alma
    float soulAlphaFlash; // Valor de alfa para o efeito de piscar
    float time; // Tempo restante para o fim do efeito de invencibilidade
    public float maxTime; // Tempo m�ximo de invencibilidade

    // Refer�ncia para o SpriteRenderer da alma
    [SerializeField] private SpriteRenderer soulSprite;

    bool invincible; // Indicador de invencibilidade

    // Inst�ncia est�tica para acesso global
    [HideInInspector]
    public static PlayerVars instance;

    // Chamado quando o objeto � criado
    void Awake() => instance = this;

    // Chamado no in�cio
    void Start()
    {
        // Inicializa as vari�veis de alfa da alma
        soulAlpha = soulSprite.color.a;
        soulAlphaFlash = soulAlpha / 2;

        // Armazena a cor original da alma
        soulOriginal = soulSprite.color;

        // Inicializa o tempo
        time = maxTime;
    }

    // M�todo para lidar com o dano recebido pelo jogador
    public void TakeDamage(float damageTaken)
    {
        if (!invincible)
        {
            // Reduz a vida do jogador pelo dano recebido
            health -= damageTaken;

            // Toca o som de dano
            AudioManager.instance.takingDamage();

            // Define o jogador como invenc�vel temporariamente
            invincible = true;
        }
    }

    // M�todo para piscar a alma durante o per�odo de invencibilidade
    void FlashSoul()
    {
        if (time > 0)
        {
            // Inverte o valor de alfa do efeito de piscar
            soulAlphaFlash = soulAlphaFlash * -1;

            // Aplica a cor de piscar � alma
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

        // Se o jogador estiver invenc�vel, executa o efeito de piscar da alma
        if (invincible)
        {
            FlashSoul();
        }
    }
}
