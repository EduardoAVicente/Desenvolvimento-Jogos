using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Este script em C# é responsável por
//armazenar as variáveis de ação associadas a um botão de ação durante uma batalha no jogo Unity.

public class ActVars : MonoBehaviour
{
    // Lista de textos de ação
    public List<string> actTxt;

    // Lista de valores de misericórdia
    public List<int> mercyValue;

    // Valor máximo de misericórdia
    public int mercyMax;

    // Misericórdia atual
    public int curMercy;
}
