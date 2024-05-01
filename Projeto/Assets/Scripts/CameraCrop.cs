using UnityEngine;

//Este script em C# é projetado para ajustar o recorte (crop) da câmera em Unity
//para que a imagem se ajuste ao aspect ratio desejado, mesmo se a resolução da tela não for a mesma.

[RequireComponent(typeof(Camera))]
public class CameraCrop : MonoBehaviour
{
    // Define o aspect ratio alvo (por exemplo, 16:9 ou 4:3).
    public Vector2 targetAspect = new Vector2(16, 9);
    Camera _camera;

    void Start()
    {
        // Obtém a referência para o componente Camera.
        _camera = GetComponent<Camera>();
        // Atualiza o recorte (crop) da câmera quando o script é iniciado.
        UpdateCrop();
    }

    // Método para atualizar o recorte da câmera.
    public void UpdateCrop()
    {
        // Calcula as razões entre a tela/janela e o aspect ratio alvo.
        float screenRatio = Screen.width / (float)Screen.height;
        float targetRatio = targetAspect.x / targetAspect.y;

        // Se a razão da tela/janela for aproximadamente igual ao aspect ratio alvo.
        if (Mathf.Approximately(screenRatio, targetRatio))
        {
            // Usa toda a área da tela/janela.
            _camera.rect = new Rect(0, 0, 1, 1);
        }
        // Se a razão da tela/janela for maior que o aspect ratio alvo.
        else if (screenRatio > targetRatio)
        {
            // Adiciona barras laterais (pillarbox) para manter a proporção correta.
            float normalizedWidth = targetRatio / screenRatio;
            float barThickness = (1f - normalizedWidth) / 2f;
            _camera.rect = new Rect(barThickness, 0, normalizedWidth, 1);
        }
        // Se a razão da tela/janela for menor que o aspect ratio alvo.
        else
        {
            // Adiciona barras superiores e inferiores (letterbox) para manter a proporção correta.
            float normalizedHeight = screenRatio / targetRatio;
            float barThickness = (1f - normalizedHeight) / 2f;
            _camera.rect = new Rect(0, barThickness, 1, normalizedHeight);
        }
    }
}
