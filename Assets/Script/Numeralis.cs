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

    private Rigidbody2D rb;
    private bool isDashing = false;

    [Header("Ground Properties")]
    public LayerMask groundLayer;
    public float groundDistance;
    public bool isGrounded;
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
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 moveVelocity = moveInput.normalized * (isDashing ? dashSpeed : speed);

        rb.velocity = moveVelocity;

        Numeralis.SetFloat("input_x", moveInput.x);
        Numeralis.SetFloat("input_y", moveInput.y);
        Numeralis.SetBool("isWalking", moveInput.magnitude > 0);

        // Verifica se a tecla Shift está sendo pressionada e o jogador está em movimento.
        if (Input.GetKey(KeyCode.LeftShift) && moveInput.magnitude > 0)
        {
            StartCoroutine(Dash());
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

            // Define a propriedade "speed" do estado "run" para 1 no Animator
            Numeralis.SetFloat("speed", 1);

            // Espera a duração do dash.
            yield return new WaitForSeconds(dashDuration);

            // Retorna à velocidade normal.
            rb.velocity = rb.velocity.normalized * originalSpeed;

            // Define a propriedade "speed" do estado "run" de volta para o valor original
            Numeralis.SetFloat("speed", originalSpeed);

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
