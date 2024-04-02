using UnityEngine;

public class Invaders : MonoBehaviour
{
    [Header("Invaders")]
    public Invader[] prefabs = new Invader[5];
    public AnimationCurve speed = new AnimationCurve();

    private Vector3 direction = Vector3.right;
    private Vector3 initialPosition;

    [Header("Grid")]
    public int rows = 5;
    public int columns = 11;

    [Header("Missiles")]
    public Projectile missilePrefab;
    public float missileSpawnRate = 1f;

    // Variáveis para o tamanho do invasor e espaçamento
    private float invaderWidth;
    private float invaderHeight;
    private float invaderSpacing = 0.009f;

    private void Awake()
    {
        initialPosition = transform.position;

        // Calcula o tamanho do invasor
        CalculateInvaderSize();

        CreateInvaderGrid();
    }

    // Método para calcular o tamanho do invasor
    private void CalculateInvaderSize()
    {
        // Verifica se há pelo menos um prefab de invasor
        if (prefabs.Length > 0 && prefabs[0] != null)
        {
            // Obtém o tamanho do sprite do primeiro invasor
            invaderWidth = prefabs[0].GetComponent<SpriteRenderer>().bounds.size.x;
            invaderHeight = prefabs[0].GetComponent<SpriteRenderer>().bounds.size.y;
        }
        else
        {
            Debug.LogError("Nenhum prefab de invasor foi configurado ou está faltando um sprite no prefab de invasor.");
        }
    }

    private void CreateInvaderGrid()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Invader invader = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);
                Vector3 position = initialPosition + new Vector3(j * (invaderWidth + invaderSpacing), -i * (invaderHeight + invaderSpacing), 0f);
                invader.transform.localPosition = position;

                // Adicionar Collider2D aos invasores
                Collider2D collider = invader.gameObject.AddComponent<BoxCollider2D>();
                collider.isTrigger = true; // Tornar o collider um trigger
            }
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), missileSpawnRate, missileSpawnRate);
    }

    private void MissileAttack()
    {
        int amountAlive = GetAliveCount();
        if (amountAlive == 0)
        {
            return;
        }

        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (Random.value < (1f / amountAlive))
            {
                Instantiate(missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }

    private void Update()
    {
        int totalCount = rows * columns;
        int amountAlive = GetAliveCount();
        int amountKilled = totalCount - amountAlive;
        float percentKilled = (float)amountKilled / (float)totalCount;

        float moveSpeed = speed.Evaluate(percentKilled);
        transform.position += moveSpeed * Time.deltaTime * direction;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (direction == Vector3.right && invader.position.x >= (rightEdge.x - 1f))
            {
                AdvanceRow();
                break;
            }
            else if (direction == Vector3.left && invader.position.x <= (leftEdge.x + 1f))
            {
                AdvanceRow();
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar se o invasor colidiu com algum objeto com tag "Barrier"
        if (collision.CompareTag("Barrier"))
        {
            // Inverter a direção
            direction *= -1;
        }
    }

    private void AdvanceRow()
    {
        // Reposicionar os invasores
        RepositionInvaders();

        // Inverter a direção
        direction *= -1;
    }

    private void RepositionInvaders()
    {
        // Obter a posição atual dos invasores
        Vector3 position = transform.position;

        // Iterar sobre todos os invasores e reposicioná-los
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                int index = i * columns + j;
                Transform invader = transform.GetChild(index);

                if (invader.gameObject.activeInHierarchy)
                {
                    Vector3 invaderPosition = invader.position;
                    // Ajustar a posição com base no passo atual do movimento
                    if (direction == Vector3.right || direction == Vector3.left)
                    {
                        invaderPosition.x = position.x + j * (invaderWidth + invaderSpacing);
                    }
                    else
                    {
                        invaderPosition.y = position.y - i * (invaderHeight + invaderSpacing);
                    }
                    invader.position = invaderPosition;
                }
            }
        }
    }

    public void ResetInvaders()
    {
        direction = Vector3.right;

        // Destruir todos os invasores existentes
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Criar novos invasores nas posições iniciais
        CreateInvaderGrid();
    }

    public int GetAliveCount()
    {
        int count = 0;
        foreach (Transform invader in transform)
        {
            if (invader.gameObject.activeSelf)
            {
                count++;
            }
        }
        return count;
    }
}
