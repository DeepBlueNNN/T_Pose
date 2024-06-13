using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject playerPivot;
    public GameObject cameraRoot;

    private Transform _playerTr;
    private Transform _cameraTr;

    // Camera 회전 관련
    public float cameraYaw;
    public float cameraPitch;

    [SerializeField]
    private float rotSensitive = 2f;//카메라 회전 감도
    [SerializeField]
    private float RotationMin = -10f;//카메라 회전각도 최소
    [SerializeField]
    private float RotationMax = 60f;//카메라 회전각도 최대
    [SerializeField]
    private float smoothTime = 0.5f;//카메라가 회전하는데 걸리는 시간

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
    /// 카메라의 위아래 시점 변경에 관한 함수
    /// </summary>
    private void CameraRotation()
    {
        cameraYaw = cameraYaw + Input.GetAxis("Mouse X") * rotSensitive;//마우스 좌우움직임을 입력받아서 카메라의 Y축을 회전시킨다
        cameraPitch = cameraPitch - Input.GetAxis("Mouse Y") * rotSensitive;//마우스 상하움직임을 입력받아서 카메라의 X축을 회전시킨다
        // pitch회전값은 마우스를 아래로 했을때(음수값이 입력 받아질때) 값이 더해져야 카메라가 아래로 회전한다 

        // pitch회전값 제한
        cameraPitch = Mathf.Clamp(cameraPitch, RotationMin, RotationMax);

        //SmoothDamp를 통해 부드러운 카메라 회전
        _targetRotation = Vector3.SmoothDamp(_targetRotation, new Vector3(cameraPitch, cameraYaw), ref _currentVel, smoothTime);
        _cameraTr.eulerAngles = _targetRotation;

        // player pivot의 yaw를 camera yaw만큼 회전하여 control yaw rotate 잠금
        _playerTr.eulerAngles = new Vector3(0, _targetRotation.y, 0);
    }
}
