using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Blocos : MonoBehaviour
{
    
    public int vida = 2;


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Ball"))
        {
            vida--;
            
            switch (vida)
            {
                case 1:
                    MudarCor("#02CD00");
                    break;
                case 2:
                    MudarCor("#FFEE00");
                    break;
                case 3:
                    MudarCor("#0004FF");
                    break;
                case 4:
                    MudarCor("#FF00B5");
                    break;
                default:
                    Destroy(gameObject);
                    break;
            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Código de atualização
    }

    public void MudarCor(string corHexadecimal)
    {
        Color cor;
        if (ColorUtility.TryParseHtmlString(corHexadecimal, out cor))
        {
            GetComponent<Renderer>().material.color = cor;
        }

        GetComponent<SpriteRenderer>().color = HexToColor(corHexadecimal);
    }

    Color HexToColor(string hex)
    {
        // Remove o "#" do início, se estiver presente
        if (hex.StartsWith("#"))
        {
            hex = hex.Substring(1);
        }

        // Converte o hexadecimal para Color
        float r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
        float g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
        float b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) / 255f;

        // Retorna a cor
        return new Color(r, g, b);
    }



    public static int totalBlocks = 0;

    void Start()
    {
        totalBlocks++;
    }

    void OnDestroy()
    {
        totalBlocks--;

        if (totalBlocks <= 0)
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // Você precisa definir as próximas cenas no Editor do Unity
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Verifica se a próxima cena existe
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Não há mais cenas disponíveis!");
        }
    }


}
