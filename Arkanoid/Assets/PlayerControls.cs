using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed = 10.0f;
    private Rigidbody2D rb2d;

    // Limites de movimento
    public float minX = -1.60f;
    public float maxX = 1.7f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Obter a posição do mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Limitar a posição do jogador
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(mousePos.x, minX, maxX);

        // Aplicar a nova posição
        transform.position = pos;
    }
}
