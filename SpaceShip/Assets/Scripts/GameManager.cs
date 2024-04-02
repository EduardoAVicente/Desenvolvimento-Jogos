using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;
    [SerializeField] private Parallax[] backgroundParallax; // Array de objetos Parallax para o fundo

    private Player player;
    private Invaders invaders;
    private MysteryShip mysteryShip;

    private int score;
    private int lives;
    private float parallaxSpeedMultiplier = 1f; // Multiplicador de velocidade inicial

    public int Score => score;
    public int Lives => lives;
    public float ParallaxSpeedMultiplier => parallaxSpeedMultiplier; // Propriedade para acessar o multiplicador de velocidade

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        invaders = FindObjectOfType<Invaders>();
        mysteryShip = FindObjectOfType<MysteryShip>();

        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }

        // Atualizar o multiplicador de velocidade do paralaxe com base na pontuação
        UpdateParallaxSpeedMultiplier();
    }

    private void UpdateParallaxSpeedMultiplier()
    {
        // Definir o multiplicador de velocidade do paralaxe com base na pontuação atual
        if (score == 120 
            || score == 210
            || score == 300
            || score == 420
            || score == 510
            || score == 600
            || score == 720
            || score == 810
            || score == 900
            || score == 1020
            || score == 1110
            || score == 1200
            || score == 1210
            || score == 1300
            || score == 1420
            || score == 1510
            || score == 1600
            )
        {
            parallaxSpeedMultiplier = 0.5f; // Reduzir para metade quando a pontuação atingir 100
            // Agendar o retorno ao valor normal após 5 segundos
            Invoke(nameof(RestoreNormalParallax), 5f);
        }

        // Atualizar a velocidade do paralaxe em todos os objetos do fundo
        foreach (Parallax parallaxObject in backgroundParallax)
        {
            parallaxObject.SetParallaxSpeedMultiplier(parallaxSpeedMultiplier);
        }
    }

    // Método para restaurar o efeito de paralaxe ao valor normal
    private void RestoreNormalParallax()
    {
        parallaxSpeedMultiplier = 1f; // Restaurar o efeito de paralaxe normal

        // Atualizar a velocidade do paralaxe em todos os objetos do fundo
        foreach (Parallax parallaxObject in backgroundParallax)
        {
            parallaxObject.SetParallaxSpeedMultiplier(parallaxSpeedMultiplier);
        }
    }
    private void NewGame()
    {
        gameOverUI.SetActive(false);
        winUI.SetActive(false);
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        invaders.ResetInvaders();
        invaders.gameObject.SetActive(true);

        Respawn();
    }

    private void Respawn()
    {
        Vector3 position = player.transform.position;
        position.x = -3.68f;
        player.transform.position = position;
        player.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        gameOverUI.SetActive(true);
        invaders.gameObject.SetActive(false);

        // Permitir que o jogador inicie um novo jogo pressionando Enter
        StartCoroutine(WaitForRestart());
    }

    private void Win()
    {
        winUI.SetActive(true);
        invaders.gameObject.SetActive(false);

        // Permitir que o jogador inicie um novo jogo pressionando Enter
        StartCoroutine(WaitForRestart());
    }

    // Método para aguardar até que o jogador pressione Enter para reiniciar o jogo
    private IEnumerator WaitForRestart()
    {
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }

        // Reiniciar o jogo quando o jogador pressionar Enter
        NewGame();
    }


    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(4, '0');
    }

    private void SetLives(int lives)
    {
        this.lives = Mathf.Max(lives, 0);
        livesText.text = this.lives.ToString();
    }

    public void OnPlayerKilled(Player player)
    {
        SetLives(lives - 1);

        player.gameObject.SetActive(false);

        if (lives > 0)
        {
            Invoke(nameof(NewRound), 1f);
        }
        else
        {
            GameOver();
        }
    }

    public void OnInvaderKilled(Invader invader)
    {
        invader.gameObject.SetActive(false);

        SetScore(score + invader.score);

        if (invaders.GetAliveCount() == 0)
        {
            Win(); // Chamar Win() quando não houver mais invasores
        }
    }


    public void OnMysteryShipKilled(MysteryShip mysteryShip)
    {
        SetScore(score + mysteryShip.score);
    }

    public void OnBoundaryReached()
    {
        if (invaders.gameObject.activeSelf)
        {
            invaders.gameObject.SetActive(false);

            OnPlayerKilled(player);
        }
    }
}
