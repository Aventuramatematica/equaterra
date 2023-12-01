using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Movement Properties")]
    public Animator Numeralis;
    float input_x = 0;
    float input_y = 0;
    public float speed = 2.5f;
    bool isWalking = false;

    // Start is called before the first frame update
    void Start()
    {
        isWalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        input_x = Input.GetAxisRaw("Horizontal");
        input_y = Input.GetAxisRaw("Vertical");
        isWalking = (input_x != 0 || input_y != 0);

        if (isWalking)
        {
            var move = new Vector3(input_x, input_y, 0).normalized;
            transform.position += move * speed * Time.deltaTime;
            Numeralis.SetFloat("input_x", input_x);
            Numeralis.SetFloat("input_y", input_y);
        }

        Numeralis.SetBool("isWalking", isWalking);


    }
}