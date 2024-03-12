using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject ballPrefab; // Prefab da bola
    public Transform ballSpawnPoint; // Ponto de spawn da bola
    public float timeToIncreaseLevel = 5; // Tempo para aumentar o n�vel
    public float timeDecreaseRate = 20f; // Taxa de diminui��o do tempo

    public float timeSinceLastPoint = 0f;
    private float timer = 0f;
    private int currentLevel = 1;

    private void Update()
    {
        timer += Time.deltaTime;
        timeSinceLastPoint += Time.deltaTime;

        // Verifica o n�vel atual e adiciona mais bolas conforme necess�rio
        if (timeSinceLastPoint >= timeToIncreaseLevel)
        {
            IncreaseLevel();
            timeSinceLastPoint = 0f;
            timeToIncreaseLevel -= timeDecreaseRate; // Diminui o tempo para aumentar o n�vel
        }
    }

    private void IncreaseLevel()
    {
        currentLevel++;
        Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);
        FindObjectOfType<BallControl>().AddBall(); // Chamada para adicionar outra bola
    }
    
    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
