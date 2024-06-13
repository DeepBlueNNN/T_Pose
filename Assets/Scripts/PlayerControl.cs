using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float amount = 0f;      // 회전 관련 변수

    [SerializeField]
    private float speed = 300f;     // 이동 속도 변수
    [SerializeField]
    private float jumpPower = 4f;   // 점프에 가하는 힘 변수
    private bool isJumping = false;

    [SerializeField]
    private float _hMoveSpeed;      // 좌,우 이동 속도 값
    [SerializeField]    
    private float _vMoveSpeed;      // 앞,뒤 이동 속도 값

    public GameObject playerPivot;
    public GameObject playerModel;

    private Rigidbody rb;

    void Start()
    {
        rb = playerModel.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // player pivot의 위치를 player model의 위치와 일치시킴
        playerPivot.transform.position = 
            new Vector3(playerModel.transform.position.x, playerModel.transform.position.y - 1.8f, playerModel.transform.position.z);


        // t-pose 팽이 사출
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

        // 키보드 조작에 따라 horizontal, vertical 방향의 move speed 결정
        _hMoveSpeed = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        _vMoveSpeed = Input.GetAxis("Vertical") * speed * Time.deltaTime;


        // player 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(playerPivot.transform.up * jumpPower, ForceMode.Impulse);
        }

    }

    void FixedUpdate()
    {
        // amount 값에 따라 player model을 지정한 방향으로 회전
        float _rotateAmount = amount * Time.deltaTime;
        rb.AddTorque(playerPivot.transform.up * _rotateAmount, ForceMode.Force);
        //rb.AddTorque(transform.up * _rotateAmount, ForceMode.Force);
        //rb.AddRelativeTorque(Vector3.up * _rotateAmount, ForceMode.Force);


        //rb.AddForce(_hRotateSpeed, 0.0f, _vRotateSpeed, ForceMode.Acceleration);
        rb.AddForce(_hMoveSpeed * playerPivot.transform.right, ForceMode.Acceleration);     // player 가로축 이동 (좌, 우)
        rb.AddForce(_vMoveSpeed * playerPivot.transform.forward, ForceMode.Acceleration);   // player 세로축 이동 (앞, 뒤)
    }

}
