using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float amount = 0f;
    public float speed = 300f;
    public float jumpPower = 4f;

    public float _hRotateSpeed;
    public float _vRotateSpeed;

    public GameObject playerPivot;
    public GameObject playerModel;

    private Rigidbody rb;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //rb = playerModel.GetComponentInChildren<Rigidbody>();
        rb = playerModel.GetComponent<Rigidbody>();
    }

    void Update()
    {
        playerPivot.transform.position = 
            new Vector3(playerModel.transform.position.x, playerModel.transform.position.y - 1.8f, playerModel.transform.position.z);


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (rb.useGravity)
            {
                rb.useGravity = false;
            }
            else
            {
                rb.useGravity = true;
            }
        }


        _hRotateSpeed = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        _vRotateSpeed = Input.GetAxis("Vertical") * speed * Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        }

    }

    void FixedUpdate()
    {
        float _rotateAmount = amount * Time.deltaTime;
        rb.AddTorque(transform.up * _rotateAmount, ForceMode.Force);


        rb.AddForce(_hRotateSpeed, 0.0f, _vRotateSpeed, ForceMode.Acceleration);

       
        //Debug.Log(Input.GetAxis("Horizontal"));
        //Debug.Log(Input.GetAxis("Vertical"));
    }

}
