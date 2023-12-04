using System.Collections;
using UnityEngine;

public enum GroundType
{
    None,
    Grama,
    Ladrilho,
    Terra,
    Areia,
    Cidade,
    Madeira
}

public class PlayerController : MonoBehaviour
{
    [Header("Movement Properties")]
    public Animator Numeralis;
    public float speed = 2.5f;
    public float dashSpeed = 5f;
    public float dashDuration = 0.5f; // Duração do dash em segundos.
    private Vector2 lastMoveDirection = Vector2.down; // Inicializado para baixo, você pode ajustar conforme necessário.

    private Rigidbody2D rb;
    private bool isDashing = false;

    [Header("Ground Properties")]
    public LayerMask groundLayer;
    public float groundDistance;
    public bool isGrounded;
    public bool isPlayerLocked = false;
    public Vector3[] footOffSet;

    [Header("Audio")]
    [SerializeField] AudioCharacter audioPlayer = null;

    private LayerMask gramaSteps;
    private LayerMask ladrilhoSteps;
    private LayerMask terraSteps;
    private LayerMask areiaSteps;
    private LayerMask cidadeSteps;
    private LayerMask madeiraSteps;
    private GroundType groundType;
    private Collider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        gramaSteps = LayerMask.GetMask("ChaoGrama");
        ladrilhoSteps = LayerMask.GetMask("ChaoLadrilho");
        terraSteps = LayerMask.GetMask("ChaoTerra");
        areiaSteps = LayerMask.GetMask("ChaoAreia");
        cidadeSteps = LayerMask.GetMask("ChaoCidade");
        madeiraSteps = LayerMask.GetMask("ChaoMadeira");
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (isPlayerLocked) 
        {
            verticalInput = 0f;
            horizontalInput = 0f;

            Vector2 moveInputA = new Vector2(horizontalInput, verticalInput);
            Vector2 moveVelocityA = moveInputA.normalized * (isDashing ? dashSpeed : speed);

            rb.velocity = moveVelocityA;

            Numeralis.SetFloat("input_x", moveInputA.x);
            Numeralis.SetFloat("input_y", moveInputA.y);
            Numeralis.SetBool("isWalking", moveInputA.magnitude > 0);
            return;
        }
        

        // Normaliza o vetor de entrada apenas se o jogador não estiver se movendo na diagonal
        if (horizontalInput != 0 && verticalInput != 0)
        {
            if (Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput))
            {
                // Se o movimento horizontal for maior que o vertical, ajusta o vertical para zero
                verticalInput = 0f;
            }
            else
            {
                // Se o movimento vertical for maior que o horizontal, ajusta o horizontal para zero
                horizontalInput = 0f;
            }
        }

        Vector2 moveInput = new Vector2(horizontalInput, verticalInput);
        Vector2 moveVelocity = moveInput.normalized * (isDashing ? dashSpeed : speed);

        rb.velocity = moveVelocity;

        Numeralis.SetFloat("input_x", moveInput.x);
        Numeralis.SetFloat("input_y", moveInput.y);
        Numeralis.SetBool("isWalking", moveInput.magnitude > 0);

        // Verifica se a tecla Shift está sendo pressionada e o jogador está em movimento
        if (Input.GetKey(KeyCode.LeftShift) && moveInput.magnitude > 0)
        {
            StartCoroutine(Dash());
        }

        rb.velocity = moveVelocity;

        if (moveInput.magnitude > 0)
        {
            lastMoveDirection = moveInput.normalized;
        }

        Numeralis.SetFloat("input_x", lastMoveDirection.x);
        Numeralis.SetFloat("input_y", lastMoveDirection.y);
        Numeralis.SetBool("isWalking", moveInput.magnitude > 0);

        // Verifica se a tecla Shift está sendo pressionada e o jogador está em movimento
        if (Input.GetKey(KeyCode.LeftShift) && moveInput.magnitude > 0)
        {
            StartCoroutine(Dash());
        }
    }

    public void BlockNum()
    {
        Debug.Log("Chegou ate aqui sacana");
        if (isPlayerLocked)
        {
            isPlayerLocked = false;
        }
        else
        {
            isPlayerLocked = true;
        }
    }

    private void FixedUpdate()
    {
        UpdateGround();
        if (isGrounded)
            audioPlayer.PlaySteps(groundType, Mathf.Abs(rb.velocity.magnitude));
    }

    IEnumerator Dash()
    {
        if (!isDashing)
        {
            isDashing = true;

            // Salva o valor original da velocidade
            float originalSpeed = rb.velocity.magnitude;

            // Aumenta a velocidade durante o dash.
            rb.velocity = rb.velocity.normalized * dashSpeed;

            // Espera a duração do dash.
            yield return new WaitForSeconds(dashDuration);

            // Retorna à velocidade normal.
            rb.velocity = rb.velocity.normalized * originalSpeed;

            isDashing = false;
        }
    }

    private void UpdateGround()
    {
        if (col.IsTouchingLayers(gramaSteps))
            groundType = GroundType.Grama;
        else if (col.IsTouchingLayers(ladrilhoSteps))
            groundType = GroundType.Ladrilho;
        else if (col.IsTouchingLayers(terraSteps))
            groundType = GroundType.Terra;
        else if (col.IsTouchingLayers(areiaSteps))
            groundType = GroundType.Areia;
        else if (col.IsTouchingLayers(cidadeSteps))
            groundType = GroundType.Cidade;
        else if (col.IsTouchingLayers(madeiraSteps))
            groundType = GroundType.Madeira;
        else
            groundType = GroundType.None;
    }
}
