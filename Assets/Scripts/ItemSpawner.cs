using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner instance;

    [Header("Arraste os Prefabs aqui")]
    public GameObject queijoPrefab;
    public GameObject ratoeiraPrefab;
    public GameObject queijoDouradoPrefab;

    [Header("Limites da Tela para Nascerem Itens")]
    public float minX = -6.55f;
    public float maxX = 6.6f;
    public float minY = -3.9f;
    public float maxY = 3.9f;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        
        GameController.Init();
        
        for (int i = 0; i < 3; i++)
        {
            GerarItem(queijoPrefab);
            GerarItem(ratoeiraPrefab);
        }
    }

    public void SpawnNovoQueijo()
    {
        GerarItem(queijoPrefab);
        
        int chanceDeRatoeira = 15 + (GameController.pontos * 3);

        if (chanceDeRatoeira > 75)
        {
            chanceDeRatoeira = 75;
        }

        if (Random.Range(0, 100) < chanceDeRatoeira)
        {
            GerarItem(ratoeiraPrefab);
        }

        if (Random.Range(0, 100) < 7)
        {
            GerarItem(queijoDouradoPrefab);
        }
    }

    private void GerarItem(GameObject prefab)
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector2 posicao = new Vector2(randomX, randomY);
        
        Instantiate(prefab, posicao, Quaternion.identity);
    }
}