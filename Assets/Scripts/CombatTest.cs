using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTest : MonoBehaviour
{
    [SerializeField]
    private float amount = 0f;      // ȸ�� ���� ����

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
        // player pivot�� ��ġ�� player model�� ��ġ�� ��ġ��Ŵ
        playerPivot.transform.position =
            new Vector3(playerModel.transform.position.x, playerModel.transform.position.y - 1.8f, playerModel.transform.position.z);
    }

    private void FixedUpdate()
    {
        // amount ���� ���� player model�� ������ �������� ȸ��
        float _rotateAmount = amount * Time.deltaTime;
        rb.AddTorque(playerPivot.transform.up * _rotateAmount, ForceMode.Force);
    }
}
