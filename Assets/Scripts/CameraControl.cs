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
    public float cameraYaxis;
    public float cameraXaxis;

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

    //[SerializeField]
    //private float lookSensitivity;
    //[SerializeField]
    //private float cameraRotationLimit;
    //private float _currentCameraRotationX;

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
        cameraYaxis = cameraYaxis + Input.GetAxis("Mouse X") * rotSensitive;//���콺 �¿�������� �Է¹޾Ƽ� ī�޶��� Y���� ȸ����Ų��
        cameraXaxis = cameraXaxis - Input.GetAxis("Mouse Y") * rotSensitive;//���콺 ���Ͽ������� �Է¹޾Ƽ� ī�޶��� X���� ȸ����Ų��
        //Xaxis�� ���콺�� �Ʒ��� ������(�������� �Է� �޾�����) ���� �������� ī�޶� �Ʒ��� ȸ���Ѵ� 

        //X��ȸ���� �Ѱ�ġ�� �����ʰ� �������ش�.
        cameraXaxis = Mathf.Clamp(cameraXaxis, RotationMin, RotationMax);

        //SmoothDamp�� ���� �ε巯�� ī�޶� ȸ��
        _targetRotation = Vector3.SmoothDamp(_targetRotation, new Vector3(cameraXaxis, cameraYaxis), ref _currentVel, smoothTime);
        _cameraTr.eulerAngles = _targetRotation;


        //_currentCameraRotationX -= _cameraRotationX;
        //_currentCameraRotationX = Mathf.Clamp(_currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        ////cameraRoot.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0f, 0f);
        //cameraRoot.transform.localEulerAngles = new Vector3(_currentCameraRotationX, _cameraRotationY, 0f);
    }
}
