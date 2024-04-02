using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;
    private float movingSpeed = 5f;
    public float parallaxEffect;
    private float normalParallaxEffect;
    public float speedMultiplier = 1f;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        normalParallaxEffect = parallaxEffect; // Salvando o valor do efeito de paralaxe normal
    }

    void Update()
    {
        // Definir a velocidade de acordo com o multiplicador de velocidade e o efeito de paralaxe
        float speed = movingSpeed * speedMultiplier * parallaxEffect;

        // Mover o fundo
        transform.position += Vector3.left * Time.deltaTime * speed;

        // Verificar se o fundo se moveu completamente para a esquerda e reposicioná-lo
        if (transform.position.x < startPos - length)
        {
            transform.position = new Vector3(startPos, transform.position.y, transform.position.z);
        }
    }

    // Método para definir o multiplicador de velocidade do paralaxe
    public void SetParallaxSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}
