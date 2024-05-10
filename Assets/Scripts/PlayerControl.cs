using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float amount = 0f;
    public float dec_power = 0.1f;
    public float speed = 300f;
    public float jumpPower = 4f;

    public float _hRotateSpeed;
    public float _vRotateSpeed;

    public GameObject playerModel;
    public GameObject cameraRoot;

    private Rigidbody rb;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //rb = GetComponentInChildren<Rigidbody>();
        rb = playerModel.GetComponent<Rigidbody>();
    }

    void Update()
    {
        cameraRoot.transform.position = playerModel.transform.position;

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

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            amount += 10f;
        }

        _hRotateSpeed = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        _vRotateSpeed = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        }

        //transform.Rotate(_vRotateSpeed, 0f, _hRotateSpeed);
        //transform.rotation = Quaternion.Euler(new Vector3(_vRotateSpeed, 0, _hRotateSpeed));

    }

    void FixedUpdate()
    {
        float _rotateAmount = amount * Time.deltaTime;

        rb.AddTorque(transform.up * _rotateAmount, ForceMode.Force);

        if (amount > 0.1f)
        {
            amount -= dec_power;

            if (amount < 0.1f && amount > 0f)
            {
                amount = 0f;
            }
        }

        rb.AddForce(_hRotateSpeed, 0.0f, _vRotateSpeed, ForceMode.Acceleration);

       

        //Debug.Log(Input.GetAxis("Horizontal"));
        //Debug.Log(Input.GetAxis("Vertical"));
    }
}
