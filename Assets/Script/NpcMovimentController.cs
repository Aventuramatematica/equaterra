using UnityEngine;
using System.Collections.Generic;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float moveTime = 2f;
    [SerializeField] private float maxDistance = 30;
    [SerializeField] private Sprite spriteNorteR;
    [SerializeField] private Sprite spriteNorteL;
    [SerializeField] private Sprite spriteLesteR;
    [SerializeField] private Sprite spriteLesteL;
    [SerializeField] private Sprite spriteSulR;
    [SerializeField] private Sprite spriteSulL;
    [SerializeField] private Sprite spriteOesteR;
    [SerializeField] private Sprite spriteOesteL;
    [SerializeField] private float velocity = 0.245f;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private int moving = 0;
    private float timer;
    private float timer_foot;
    private bool r;
    private System.Random random = new System.Random();
    private Vector2 initialPosition;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = moveTime;
        timer_foot = velocity;
        r = true;
        initialPosition = transform.position;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            moving = random.Next(0, 4);
            timer = moveTime;
        }

        MoveNPC();

        if (Vector2.Distance(transform.position, initialPosition) > maxDistance)
        {
            transform.position = initialPosition;
        }

        timer_foot -= Time.deltaTime;

        if (timer_foot <= 0f)
        {
            timer_foot = velocity;
            r = !r;
        }

        UpdateSprite();
    }

    void UpdateSprite()
    {
        if (moving == 0)
        {
            spriteRenderer.sprite = r ? spriteNorteR : spriteNorteL;
        }
        else if (moving == 1)
        {
            spriteRenderer.sprite = r ? spriteLesteR : spriteLesteL;
        }
        else if (moving == 2)
        {
            spriteRenderer.sprite = r ? spriteSulR : spriteSulL;
        }
        else if (moving == 3)
        {
            spriteRenderer.sprite = r ? spriteOesteR : spriteOesteL;
        }
    }

    void MoveNPC()
    {
        float verticalMovement = 0f;
        float horizontalMovement = 0f;

        if (moving == 0)
        {
            verticalMovement = 1f;
        }
        else if (moving == 1)
        {
            horizontalMovement = 1f;
        }
        else if (moving == 2)
        {
            verticalMovement = -1f;
        }
        else if (moving == 3)
        {
            horizontalMovement = -1f;
        }

        Vector2 movement = new Vector2(horizontalMovement, verticalMovement);
        rb2d.velocity = movement * speed;
    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            // Se colidir com algo que não é o jogador, muda imediatamente de direção.
            moving = random.Next(0, 4);
        }
    }
}
