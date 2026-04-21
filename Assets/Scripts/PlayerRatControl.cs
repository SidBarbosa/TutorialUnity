using UnityEngine;

public class PlayerRatControl : MonoBehaviour
{
    [Header("Configurações")]
    public float moveSpeed = 5f;
    
    [Header("Sprites de Direção")]
    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteLeft;
    public Sprite spriteRight;
    public Sprite spriteUpLeft;
    public Sprite spriteUpRight;
    public Sprite spriteDownLeft;
    public Sprite spriteDownRight;

    [Header("Sons")]
    public AudioClip somColeta;
    public AudioClip somDano;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private AudioSource audioSource;
    private Vector2 movementInput;
    private Sprite lastDirectionSprite;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>(); // Pega o emissor de som
        lastDirectionSprite = spriteUp;
    }

    private void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput = movementInput.normalized;
        UpdatePlayerSprite();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movementInput * moveSpeed;
    }

    private void UpdatePlayerSprite()
    {
        if (movementInput == Vector2.zero)
        {
            sr.sprite = lastDirectionSprite;
            return;
        }

        if (movementInput.x == 0 && movementInput.y > 0) sr.sprite = spriteUp;
        else if (movementInput.x == 0 && movementInput.y < 0) sr.sprite = spriteDown;
        else if (movementInput.x < 0 && movementInput.y == 0) sr.sprite = spriteLeft;
        else if (movementInput.x > 0 && movementInput.y == 0) sr.sprite = spriteRight;
        else if (movementInput.x < 0 && movementInput.y > 0) sr.sprite = spriteUpLeft;
        else if (movementInput.x > 0 && movementInput.y > 0) sr.sprite = spriteUpRight;
        else if (movementInput.x < 0 && movementInput.y < 0) sr.sprite = spriteDownLeft;
        else if (movementInput.x > 0 && movementInput.y < 0) sr.sprite = spriteDownRight;

        lastDirectionSprite = sr.sprite;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("coletavel"))
        {
            audioSource.PlayOneShot(somColeta);
            GameController.AdicionarPonto();
            
            if(ItemSpawner.instance != null) ItemSpawner.instance.SpawnNovoQueijo();
            
            Destroy(other.gameObject);
        }
        // 2. Se bater na Ratoeira
        else if (other.CompareTag("inimigo"))
        {
            audioSource.PlayOneShot(somDano);
            GameController.PerderVida();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("queijoDourado"))
        {
            audioSource.PlayOneShot(somColeta); 
            
            GameController.pontos += 5; 

            GameObject[] todasAsRatoeiras = GameObject.FindGameObjectsWithTag("inimigo");
            
            foreach (GameObject ratoeira in todasAsRatoeiras)
            {
                Destroy(ratoeira);
            }
            Destroy(other.gameObject);
        }
    }
}