using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject playerPivot;
    public GameObject cameraRoot;

    private Transform _playerTr;
    private Transform _cameraTr;

    // Camera ȸ�� ����
    public float cameraYaw;
    public float cameraPitch;

    [SerializeField]
    private float rotSensitive = 2f;//ī�޶� ȸ�� ����
    [SerializeField]
    private float RotationMin = -10f;//ī�޶� ȸ������ �ּ�
    [SerializeField]
    private float RotationMax = 60f;//ī�޶� ȸ������ �ִ�
    [SerializeField]
    private float smoothTime = 0.5f;//ī�޶� ȸ���ϴµ� �ɸ��� �ð�

    private Vector3 _targetRotation;
    private Vector3 _currentVel;


    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;   

        _playerTr = playerPivot.GetComponent<Transform>();
        _cameraTr = cameraRoot.GetComponent<Transform>();
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        _cameraTr.position = _playerTr.position;
        CameraRotation();
    }

    /// <summary>
    /// ī�޶��� ���Ʒ� ���� ���濡 ���� �Լ�
    /// </summary>
    private void CameraRotation()
    {
        cameraYaw = cameraYaw + Input.GetAxis("Mouse X") * rotSensitive;//���콺 �¿�������� �Է¹޾Ƽ� ī�޶��� Y���� ȸ����Ų��
        cameraPitch = cameraPitch - Input.GetAxis("Mouse Y") * rotSensitive;//���콺 ���Ͽ������� �Է¹޾Ƽ� ī�޶��� X���� ȸ����Ų��
        // pitchȸ������ ���콺�� �Ʒ��� ������(�������� �Է� �޾�����) ���� �������� ī�޶� �Ʒ��� ȸ���Ѵ� 

        // pitchȸ���� ����
        cameraPitch = Mathf.Clamp(cameraPitch, RotationMin, RotationMax);

        //SmoothDamp�� ���� �ε巯�� ī�޶� ȸ��
        _targetRotation = Vector3.SmoothDamp(_targetRotation, new Vector3(cameraPitch, cameraYaw), ref _currentVel, smoothTime);
        _cameraTr.eulerAngles = _targetRotation;

        // player pivot�� yaw�� camera yaw��ŭ ȸ���Ͽ� control yaw rotate ���
        _playerTr.eulerAngles = new Vector3(0, _targetRotation.y, 0);
    }
}
