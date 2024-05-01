using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

//Este script em C# é responsável por gerenciar a interação do jogador durante as ações em uma batalha no jogo

public class ActingManager : MonoBehaviour
{
    // Variáveis para gerenciar a seleção e execução de ações durante uma batalha
    int maxSelectionInt; // Valor máximo de seleção
    int minSelectionInt; // Valor mínimo de seleção
    public int selectionInt; // Valor atual de seleção
    bool isFighting; // Indicador se o jogador está lutando
    public string spareMessage; // Mensagem de poupar o inimigo
    public List<ActingButtons> buttons; // Lista de botões de ação
    public SpriteRenderer soul; // Sprite da alma do jogador
    public TextMeshPro actingText; // Texto de ação
    public bool isActing; // Indicador se o jogador está realizando uma ação
    public GameObject actObjects; // Objeto contendo os elementos relacionados à ação
    public int totalMercy; // Total de misericórdia concedida
    public int totalMercyMax; // Total máximo de misericórdia concedida
    public List<string> flavorText; // Lista de textos de sabor
    public float time; // Tempo decorrido
    public bool canAct = true; // Indicador se o jogador pode realizar uma ação

    void Start()
    {
        // Obtém o estado da batalha do gerenciador de batalha
        isFighting = BattleManager.battleInstance.isFighting;

        // Define os valores máximo e mínimo de seleção
        maxSelectionInt = 3;
        minSelectionInt = 0;
    }

    void Update()
    {
        if (!isFighting && isActing)
        {
            // Verifica se a seleção está dentro dos limites permitidos
            if (selectionInt > maxSelectionInt)
            {
                selectionInt = 0;
            }
            if (selectionInt < minSelectionInt)
            {
                selectionInt = 3;
            }

            // Processa a entrada do jogador para mover a seleção
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

            // Realiza a seleção e execução da ação
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

    // Método para selecionar visualmente a ação atual
    void Selecting(int selectedInt)
    {
        if (buttons[selectedInt].selected)
        {
            soul.transform.position = buttons[selectedInt].soulPosition.position;
        }
    }

    // Método para deselecionar uma ação
    void Deselecting(int deselectionInt)
    {
        buttons[deselectionInt].selected = false;
    }

    // Método para realizar a seleção visual das ações disponíveis
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

    // Método para executar a ação selecionada
    void Selected()
    {
        if (selectionInt >= 0 && selectionInt < buttons.Count)
        {
            OnActing(selectionInt);
        }
    }

    // Método para realizar uma ação específica
    public void OnActing(int selectedInt)
    {
        canAct = false;

        // Adiciona misericórdia conforme necessário
        buttons[selectedInt].actVars.curMercy += buttons[selectedInt].actVars.mercyValue[0];

        // Ativa o texto de ação e define o texto
        actingText.gameObject.SetActive(true);
        DialogueManager.instance.dialogueTxt = buttons[selectedInt].actVars.actTxt[0];

        // Inicia o diálogo com o inimigo
        Action doneTalking = () =>
        {
            Debug.Log("action initiated");
            DialogueManager.instance.shouldTalk = false;
            StartCoroutine(BattleManager.battleInstance.ActingSequence());
        };
        DialogueManager.instance.enemyTxt = BattleManager.battleInstance.enemyDialogue[UnityEngine.Random.Range(0, BattleManager.battleInstance.enemyDialogue.Count)];
        DialogueManager.instance.shouldTalk = true;
        DialogueManager.instance.Talking(doneTalking);

        // Desativa os objetos relacionados à ação
        actObjects.SetActive(false);

        // Atualiza as listas de texto de ação e valores de misericórdia
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
