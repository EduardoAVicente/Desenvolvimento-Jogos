using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Este script em C# � respons�vel por
//armazenar as vari�veis de a��o associadas a um bot�o de a��o durante uma batalha no jogo Unity.

public class ActVars : MonoBehaviour
{
    // Lista de textos de a��o
    public List<string> actTxt;

    // Lista de valores de miseric�rdia
    public List<int> mercyValue;

    // Valor m�ximo de miseric�rdia
    public int mercyMax;

    // Miseric�rdia atual
    public int curMercy;
}
