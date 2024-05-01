using UnityEngine;
using UnityEngine.SceneManagement;

public class Passagem : MonoBehaviour
{
    // Posição de retorno na passagem
    public Vector3 posicaoDeRetorno;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            int previousSceneIndex = currentSceneIndex - 1;

            // Verifica se o collider do objeto de passagem tem a tag "Passagem" ou "PassagemDeVolta"
            if (gameObject.CompareTag("Passagem"))
            {
                // Configura a posição de retorno antes de carregar a próxima cena
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    player.posicaoDeRetorno = posicaoDeRetorno;
                }

                // Certifica-se de que a próxima cena existe antes de carregar
                if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(nextSceneIndex);
                }
                else
                {
                    Debug.LogWarning("Não há próxima cena configurada.");
                }
            }
            else if (gameObject.CompareTag("PassagemDeVolta"))
            {
                // Certifica-se de que há uma cena anterior para voltar
                if (previousSceneIndex >= 0)
                {
                    // Carrega a cena anterior
                    SceneManager.LoadScene(previousSceneIndex);
                }
                else
                {
                    Debug.LogWarning("Não há cena anterior configurada.");
                }
            }
        }
    }
}
