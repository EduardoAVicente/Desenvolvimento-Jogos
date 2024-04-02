using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Invader : MonoBehaviour
{
    public float animationSpeed = 1f;
    public int score = 10;

    private void Start()
    {
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto colidido é um laser do jogador
        if (other.CompareTag("PlayerLaser"))
        {
            // Desativa o invasor
            gameObject.SetActive(false);

            // Desativa também o laser do jogador
            other.gameObject.SetActive(false);

            // Informa ao GameManager que um invasor foi morto
            GameManager.Instance.OnInvaderKilled(this);
        }
    }
}
