using UnityEngine;

//Este script em C# � projetado para ajustar o recorte (crop) da c�mera em Unity
//para que a imagem se ajuste ao aspect ratio desejado, mesmo se a resolu��o da tela n�o for a mesma.

[RequireComponent(typeof(Camera))]
public class CameraCrop : MonoBehaviour
{
    // Define o aspect ratio alvo (por exemplo, 16:9 ou 4:3).
    public Vector2 targetAspect = new Vector2(16, 9);
    Camera _camera;

    void Start()
    {
        // Obt�m a refer�ncia para o componente Camera.
        _camera = GetComponent<Camera>();
        // Atualiza o recorte (crop) da c�mera quando o script � iniciado.
        UpdateCrop();
    }

    // M�todo para atualizar o recorte da c�mera.
    public void UpdateCrop()
    {
        // Calcula as raz�es entre a tela/janela e o aspect ratio alvo.
        float screenRatio = Screen.width / (float)Screen.height;
        float targetRatio = targetAspect.x / targetAspect.y;

        // Se a raz�o da tela/janela for aproximadamente igual ao aspect ratio alvo.
        if (Mathf.Approximately(screenRatio, targetRatio))
        {
            // Usa toda a �rea da tela/janela.
            _camera.rect = new Rect(0, 0, 1, 1);
        }
        // Se a raz�o da tela/janela for maior que o aspect ratio alvo.
        else if (screenRatio > targetRatio)
        {
            // Adiciona barras laterais (pillarbox) para manter a propor��o correta.
            float normalizedWidth = targetRatio / screenRatio;
            float barThickness = (1f - normalizedWidth) / 2f;
            _camera.rect = new Rect(barThickness, 0, normalizedWidth, 1);
        }
        // Se a raz�o da tela/janela for menor que o aspect ratio alvo.
        else
        {
            // Adiciona barras superiores e inferiores (letterbox) para manter a propor��o correta.
            float normalizedHeight = screenRatio / targetRatio;
            float barThickness = (1f - normalizedHeight) / 2f;
            _camera.rect = new Rect(0, barThickness, 1, normalizedHeight);
        }
    }
}
