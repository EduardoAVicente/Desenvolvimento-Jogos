using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting; // Importa��o desnecess�ria, talvez seja removida

using UnityEngine;

//Este script parece ser um gerenciador de ataques em um jogo Unity.
//Ele � respons�vel por coordenar e controlar os ataques no jogo. 
public class AttackManager : MonoBehaviour
{
    // Refer�ncia est�tica para o pr�prio gerenciador de ataques
    public static AttackManager instance;

    // M�todo chamado quando o script � inicializado
    void Awake() => instance = this;

    // Array de prefabs de pellets (projet�is de ataque)
    public Pellet[] pelletPrefab;

    // Scriptable Object contendo informa��es sobre os ataques
    public Attacks attacksScriptable;

    // Indica se o ataque foi conclu�do
    public bool attackFinished;

    // Lista de objetos de ataque
    List<IFightObject> attackObject = new();

    // M�todo para iniciar um ataque
    public void StartAttack(IEnumerator attack, Action onFinish)
    {
        attackFinished = false;
        StartCoroutine(StartAttackEnumerator(attack, onFinish));
    }

    // M�todo chamado a cada quadro
    void Update()
    {
        // Itera sobre todos os objetos de ataque
        for (int i = 0; i < attackObject.Count; i++)
        {
            IFightObject curObject = attackObject[i];
            // Verifica se o objeto de ataque � nulo
            if (curObject == null)
                Debug.LogWarning($"THE ATTACK OBJECT AT {i} IS NULL!!!!");
            else
                // Chama o m�todo Tick() do objeto de ataque
                curObject.Tick();
        }

    }

    // M�todo para instanciar um novo pellet (proj�til de ataque)
    public void SpawnPellet(Vector2 position, PelletType type, int pelletType)
    {
        // Instancia um novo pellet na posi��o especificada
        Pellet newPellet = Instantiate(pelletPrefab[pelletType], position, Quaternion.identity).GetComponent<Pellet>();

        // Define o tipo de pellet
        newPellet.type = type;

        // Converte o pellet para um objeto de luta e o adiciona � lista de objetos de ataque
        IFightObject pelletAsObj = (IFightObject)newPellet;
        pelletAsObj.Spawn();
        attackObject.Add(pelletAsObj);
    }

    // Enumerator para iniciar um ataque
    IEnumerator StartAttackEnumerator(IEnumerator attack, Action onFinish)
    {
        // Executa o ataque enquanto houverem movimentos
        while (attack.MoveNext())
        {
            yield return attack.Current;
        }

        // Chama a a��o de t�rmino do ataque
        onFinish?.Invoke();

        // Remove todos os objetos de ataque e limpa a lista
        for (int i = 0; i < attackObject.Count; i++)
            attackObject[i].Remove();
        attackObject.Clear();
    }
}
