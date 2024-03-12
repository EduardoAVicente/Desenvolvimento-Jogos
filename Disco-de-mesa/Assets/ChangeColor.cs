using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public float changeInterval = 1f; // Intervalo de mudança de cor
    public float hueStep = 0.1f; // Passo de mudança de tonalidade
    public float saturation = 1f; // Saturação constante
    public float brightness = 1f; // Brilho constante

    private SpriteRenderer spriteRenderer;
    private float timer;
    private float hue;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = changeInterval;
    }

    void Update()
    {
        // Conta o tempo para a próxima mudança de cor
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            ChangeSpriteColor();
            timer = changeInterval;
        }
    }

    void ChangeSpriteColor()
    {
        // Atualiza a tonalidade
        hue += hueStep;
        if (hue > 1f)
            hue -= 1f;

        // Converte HSB (Hue, Saturation, Brightness) para RGB
        Color newColor = Color.HSVToRGB(hue, saturation, brightness);
        // Aplica a nova cor ao sprite do objeto
        spriteRenderer.color = newColor;
    }
}
