using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermoveset : MonoBehaviour
{
    private Rigidbody2D _numeralisRigidbody2D;
    public float _numerelisSpeed;
    private Vector2 _numerelisDistance;

    // Start is called before the first frame update
    void Start()
    {
        _numeralisRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _numerelisDistance = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate()
    {
        _numeralisRigidbody2D.MovePosition(_numeralisRigidbody2D.position + _numerelisDistance * _numerelisSpeed * Time.fixedDeltaTime);
    }
}