using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float amount = 0f;      // ȸ�� ���� ����

    [SerializeField]
    private float speed = 300f;     // �̵� �ӵ� ����
    [SerializeField]
    private float jumpPower = 4f;   // ������ ���ϴ� �� ����
    private bool isJumping = false;

    [SerializeField]
    private float _hMoveSpeed;      // ��,�� �̵� �ӵ� ��
    [SerializeField]    
    private float _vMoveSpeed;      // ��,�� �̵� �ӵ� ��

    public GameObject playerPivot;
    public GameObject playerModel;

    private Rigidbody rb;

    void Start()
    {
        rb = playerModel.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // player pivot�� ��ġ�� player model�� ��ġ�� ��ġ��Ŵ
        playerPivot.transform.position = 
            new Vector3(playerModel.transform.position.x, playerModel.transform.position.y - 1.8f, playerModel.transform.position.z);


        // t-pose ���� ����
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

        // Ű���� ���ۿ� ���� horizontal, vertical ������ move speed ����
        _hMoveSpeed = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        _vMoveSpeed = Input.GetAxis("Vertical") * speed * Time.deltaTime;


        // player ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(playerPivot.transform.up * jumpPower, ForceMode.Impulse);
        }

    }

    void FixedUpdate()
    {
        // amount ���� ���� player model�� ������ �������� ȸ��
        float _rotateAmount = amount * Time.deltaTime;
        rb.AddTorque(playerPivot.transform.up * _rotateAmount, ForceMode.Force);
        //rb.AddTorque(transform.up * _rotateAmount, ForceMode.Force);
        //rb.AddRelativeTorque(Vector3.up * _rotateAmount, ForceMode.Force);


        //rb.AddForce(_hRotateSpeed, 0.0f, _vRotateSpeed, ForceMode.Acceleration);
        rb.AddForce(_hMoveSpeed * playerPivot.transform.right, ForceMode.Acceleration);     // player ������ �̵� (��, ��)
        rb.AddForce(_vMoveSpeed * playerPivot.transform.forward, ForceMode.Acceleration);   // player ������ �̵� (��, ��)
    }

}
