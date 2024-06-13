using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTest : MonoBehaviour
{
    [SerializeField]
    private float amount = 0f;      // 회전 관련 변수

    public GameObject playerPivot;
    public GameObject playerModel;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = playerModel.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // player pivot의 위치를 player model의 위치와 일치시킴
        playerPivot.transform.position =
            new Vector3(playerModel.transform.position.x, playerModel.transform.position.y - 1.8f, playerModel.transform.position.z);
    }

    private void FixedUpdate()
    {
        // amount 값에 따라 player model을 지정한 방향으로 회전
        float _rotateAmount = amount * Time.deltaTime;
        rb.AddTorque(playerPivot.transform.up * _rotateAmount, ForceMode.Force);
    }
}
