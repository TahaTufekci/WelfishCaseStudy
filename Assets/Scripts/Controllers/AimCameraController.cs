using Cinemachine;
using UnityEngine;

public class AimCameraController : MonoBehaviour
{
    public CinemachineVirtualCamera normalVirtualCamera;
    public CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private Transform cameraTarget;
    private float sensitivity = 100f;
    private float aimSensitivity = 50f;
    private float topClamp = 70.0f;
    private float bottomClamp = -30.0f;
    private bool lockCameraPosition = false;
    private float cameraAngleOverride = 0.0f;

    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private const float _threshold = 0.01f;
    private bool isAiming = false;
    public Cinemachine.AxisState xAxis, yAxis;
    public Transform aimPos;
    [SerializeField] private float aimSmoothSpeed = 20f;
    [SerializeField] LayerMask aimMask;


    private void Start()
    {
        _cinemachineTargetYaw = 0f; 
        _cinemachineTargetPitch = 0f;

        cameraTarget.rotation = Quaternion.Euler(_cinemachineTargetPitch, _cinemachineTargetYaw, 0.0f);
    }
    private void Update()
    {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);
        var screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        var ray = Camera.main.ScreenPointToRay(screenCenter);
        if(Physics.Raycast(ray, out var hit, Mathf.Infinity,aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
        }
    }
    
    void LateUpdate()
    {
        HandleCameraRotation();
        HandleCameraSwitch();
    }

    private void HandleCameraRotation()
    {
        cameraTarget.localEulerAngles = new Vector3(yAxis.Value, cameraTarget.localEulerAngles.y, cameraTarget.localEulerAngles.z);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, xAxis.Value, transform.localEulerAngles.z);
    }

    private void HandleCameraSwitch()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isAiming = !isAiming;
            if (normalVirtualCamera != null)
                normalVirtualCamera.gameObject.SetActive(!isAiming);
            if (aimVirtualCamera != null)
                aimVirtualCamera.gameObject.SetActive(isAiming);
            SetSensitivity(isAiming ? aimSensitivity : 100f);
            GameManager.Instance.ChangeAimState(isAiming ? AimState.ShootingIdle : AimState.Idle);
        }
    }

    public void SetSensitivity(float newSensitivity)
    {
        sensitivity = newSensitivity;
    }
}
