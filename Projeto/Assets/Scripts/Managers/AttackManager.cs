using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting; // Importação desnecessária, talvez seja removida

using UnityEngine;

//Este script parece ser um gerenciador de ataques em um jogo Unity.
//Ele é responsável por coordenar e controlar os ataques no jogo. 
public class AttackManager : MonoBehaviour
{
    // Referência estática para o próprio gerenciador de ataques
    public static AttackManager instance;

    // Método chamado quando o script é inicializado
    void Awake() => instance = this;

    // Array de prefabs de pellets (projetéis de ataque)
    public Pellet[] pelletPrefab;

    // Scriptable Object contendo informações sobre os ataques
    public Attacks attacksScriptable;

    // Indica se o ataque foi concluído
    public bool attackFinished;

    // Lista de objetos de ataque
    List<IFightObject> attackObject = new();

    // Método para iniciar um ataque
    public void StartAttack(IEnumerator attack, Action onFinish)
    {
        attackFinished = false;
        StartCoroutine(StartAttackEnumerator(attack, onFinish));
    }

    // Método chamado a cada quadro
    void Update()
    {
        // Itera sobre todos os objetos de ataque
        for (int i = 0; i < attackObject.Count; i++)
        {
            IFightObject curObject = attackObject[i];
            // Verifica se o objeto de ataque é nulo
            if (curObject == null)
                Debug.LogWarning($"THE ATTACK OBJECT AT {i} IS NULL!!!!");
            else
                // Chama o método Tick() do objeto de ataque
                curObject.Tick();
        }

    }

    // Método para instanciar um novo pellet (projétil de ataque)
    public void SpawnPellet(Vector2 position, PelletType type, int pelletType)
    {
        // Instancia um novo pellet na posição especificada
        Pellet newPellet = Instantiate(pelletPrefab[pelletType], position, Quaternion.identity).GetComponent<Pellet>();

        // Define o tipo de pellet
        newPellet.type = type;

        // Converte o pellet para um objeto de luta e o adiciona à lista de objetos de ataque
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

        // Chama a ação de término do ataque
        onFinish?.Invoke();

        // Remove todos os objetos de ataque e limpa a lista
        for (int i = 0; i < attackObject.Count; i++)
            attackObject[i].Remove();
        attackObject.Clear();
    }
}
