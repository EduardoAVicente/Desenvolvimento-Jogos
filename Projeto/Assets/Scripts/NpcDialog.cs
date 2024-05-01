using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcDialog : MonoBehaviour
{
    public string[] dialogueNpc;
    public int dialogueIndex;
    public string name;


    public GameObject dialoguePanel;
    public Text dialogueText;

    public Text nameNpc;
    public Image imageNpc;
    public Sprite spriteNpc;

    public bool readyToSpeak;
    public bool startDialogue;


    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!startDialogue)
            {

                // Encontre o jogador e trave a velocidade (substitua FindObjectOfType<Player>() pelo método apropriado)
                // FindObjectOfType<Player>().TravarVelocidade(mam);

                // Inicia o diálogo
                StartDialogue();

            }
            else if (dialogueText.text == dialogueNpc[dialogueIndex])
            {
                // Avança para o próximo diálogo
                NextDialogue();
            }

        }
    }

    void NextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex < dialogueNpc.Length)
        {
            StartCoroutine(ShowDialogue());
        }
        else
        {
            dialoguePanel.SetActive(false);
            startDialogue = false;
            dialogueIndex = 0;
            //voltar velocidade personagem
        }
    }

    void StartDialogue()
    {
        nameNpc.text = name;
        imageNpc.sprite = spriteNpc;
        startDialogue = true;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        StartCoroutine(ShowDialogue());
    }

    IEnumerator ShowDialogue()
    {
        dialogueText.text = "";
        foreach (char letter in dialogueNpc[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
