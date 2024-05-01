using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

//Este script em C# � respons�vel por gerenciar a intera��o do jogador durante as a��es em uma batalha no jogo

public class ActingManager : MonoBehaviour
{
    // Vari�veis para gerenciar a sele��o e execu��o de a��es durante uma batalha
    int maxSelectionInt; // Valor m�ximo de sele��o
    int minSelectionInt; // Valor m�nimo de sele��o
    public int selectionInt; // Valor atual de sele��o
    bool isFighting; // Indicador se o jogador est� lutando
    public string spareMessage; // Mensagem de poupar o inimigo
    public List<ActingButtons> buttons; // Lista de bot�es de a��o
    public SpriteRenderer soul; // Sprite da alma do jogador
    public TextMeshPro actingText; // Texto de a��o
    public bool isActing; // Indicador se o jogador est� realizando uma a��o
    public GameObject actObjects; // Objeto contendo os elementos relacionados � a��o
    public int totalMercy; // Total de miseric�rdia concedida
    public int totalMercyMax; // Total m�ximo de miseric�rdia concedida
    public List<string> flavorText; // Lista de textos de sabor
    public float time; // Tempo decorrido
    public bool canAct = true; // Indicador se o jogador pode realizar uma a��o

    void Start()
    {
        // Obt�m o estado da batalha do gerenciador de batalha
        isFighting = BattleManager.battleInstance.isFighting;

        // Define os valores m�ximo e m�nimo de sele��o
        maxSelectionInt = 3;
        minSelectionInt = 0;
    }

    void Update()
    {
        if (!isFighting && isActing)
        {
            // Verifica se a sele��o est� dentro dos limites permitidos
            if (selectionInt > maxSelectionInt)
            {
                selectionInt = 0;
            }
            if (selectionInt < minSelectionInt)
            {
                selectionInt = 3;
            }

            // Processa a entrada do jogador para mover a sele��o
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                selectionInt--;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                selectionInt++;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectionInt -= 2;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectionInt += 2;
            }

            // Realiza a sele��o e execu��o da a��o
            Selection();
            time += Time.deltaTime;
            if (time > 0.25f)
            {
                if (canAct)
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        canAct = false;
                        Selected();
                    }
                }
            }
        }
    }

    // M�todo para selecionar visualmente a a��o atual
    void Selecting(int selectedInt)
    {
        if (buttons[selectedInt].selected)
        {
            soul.transform.position = buttons[selectedInt].soulPosition.position;
        }
    }

    // M�todo para deselecionar uma a��o
    void Deselecting(int deselectionInt)
    {
        buttons[deselectionInt].selected = false;
    }

    // M�todo para realizar a sele��o visual das a��es dispon�veis
    void Selection()
    {
        for (int i = 0; i < 4; i++)
        {
            if (selectionInt == i)
            {
                buttons[selectionInt].selected = true;
                Selecting(selectionInt);
            }
            else
            {
                Deselecting(i);
            }
        }
    }

    // M�todo para executar a a��o selecionada
    void Selected()
    {
        if (selectionInt >= 0 && selectionInt < buttons.Count)
        {
            OnActing(selectionInt);
        }
    }

    // M�todo para realizar uma a��o espec�fica
    public void OnActing(int selectedInt)
    {
        canAct = false;

        // Adiciona miseric�rdia conforme necess�rio
        buttons[selectedInt].actVars.curMercy += buttons[selectedInt].actVars.mercyValue[0];

        // Ativa o texto de a��o e define o texto
        actingText.gameObject.SetActive(true);
        DialogueManager.instance.dialogueTxt = buttons[selectedInt].actVars.actTxt[0];

        // Inicia o di�logo com o inimigo
        Action doneTalking = () =>
        {
            Debug.Log("action initiated");
            DialogueManager.instance.shouldTalk = false;
            StartCoroutine(BattleManager.battleInstance.ActingSequence());
        };
        DialogueManager.instance.enemyTxt = BattleManager.battleInstance.enemyDialogue[UnityEngine.Random.Range(0, BattleManager.battleInstance.enemyDialogue.Count)];
        DialogueManager.instance.shouldTalk = true;
        DialogueManager.instance.Talking(doneTalking);

        // Desativa os objetos relacionados � a��o
        actObjects.SetActive(false);

        // Atualiza as listas de texto de a��o e valores de miseric�rdia
        if (buttons[selectedInt].actVars.actTxt.Count <= 2 || buttons[selectedInt].actVars.mercyValue.Count <= 2)
        {
            Debug.Log("We added");
            buttons[selectedInt].actVars.actTxt.Add(buttons[selectedInt].actVars.actTxt[0]);
            buttons[selectedInt].actVars.mercyValue.Add(buttons[selectedInt].actVars.mercyValue[0]);
        }
        else
        {
            buttons[selectedInt].actVars.actTxt.RemoveAt(0);
            buttons[selectedInt].actVars.mercyValue.RemoveAt(0);
        }
    }
}
