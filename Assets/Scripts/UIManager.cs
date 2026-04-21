using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Configurações de UI")]
    public TextMeshProUGUI textoPontos; 
    public TextMeshProUGUI textoVidas;  
    public TextMeshProUGUI textoTempo;   // NOVO: Arraste o texto do cronômetro aqui
    public TextMeshProUGUI textoFinal;   // NOVO: Arraste o texto que fica dentro do Game Over aqui
    public GameObject endGamePanel;

    [Header("Configurações de Áudio")]
    public AudioSource musicaDeFundo; 
    public AudioClip somGameOver;     
    
    private AudioSource audioSourceCanvas;
    private bool gameOverExecutado = false; 
    private float timerBonus = 0f; // Controla o bônus de tempo

    void Start()
    {
        if (endGamePanel != null) endGamePanel.SetActive(false);
        
        audioSourceCanvas = GetComponent<AudioSource>();
        
        // Garante que o tempo comece a correr ao iniciar
        Time.timeScale = 1f; 
    }

    void Update()
    {
        if (!GameController.gameOver)
        {
            if (textoPontos != null) textoPontos.text = "Queijos: " + GameController.pontos;
            if (textoVidas != null) textoVidas.text = "Vidas: " + GameController.vidas;

            GameController.tempoPartida += Time.deltaTime;
            if (textoTempo != null) 
                textoTempo.text = "Tempo: " + GameController.tempoPartida.ToString("F1") + "s";

            timerBonus += Time.deltaTime;
            if (timerBonus >= 15f)
            {
                GameController.pontos++;
                timerBonus = 0f;
            }
        }
        // Quando o jogo acaba
        if (GameController.gameOver && !gameOverExecutado)
        {
            gameOverExecutado = true; 
    
            if (endGamePanel != null) 
            {
                endGamePanel.SetActive(true);
                if (textoFinal != null)
                {
                    textoFinal.text = $"PONTOS: {GameController.pontos}\n\nTEMPO: {GameController.tempoPartida:F1}s";
                }
            }
    
            if (musicaDeFundo != null) musicaDeFundo.Stop();
    
            if (audioSourceCanvas != null && somGameOver != null)
            {
                audioSourceCanvas.PlayOneShot(somGameOver);
            }

            Time.timeScale = 0f; 
        }
    }
}