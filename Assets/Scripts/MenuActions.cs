using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    
    public void IniciaJogo()
    {
        Time.timeScale = 1f; 
        GameController.Init(); 
        SceneManager.LoadScene("SampleScene"); 
    }

    // 2. Leva para a cena de Instruções
    public void AbrirInstrucoes()
    {
        SceneManager.LoadScene("Instrucoes"); 
    }

    // 3. Volta para o Menu Principal
    public void Menu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu"); 
    }
}