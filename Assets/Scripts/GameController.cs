using UnityEngine;

public class GameController : MonoBehaviour
{
    public static int pontos;
    public static int vidas;
    public static float tempoPartida; 
    public static bool gameOver;

    public static void Init()
    {
        pontos = 0;
        vidas = 3;
        tempoPartida = 0f; 
        gameOver = false;
    }

    public static void AdicionarPonto()
    {
        pontos++;
    }

    public static void PerderVida()
    {
        vidas--;
        if (vidas <= 0)
        {
            gameOver = true;
            Time.timeScale = 0f; 
        }
    }
}