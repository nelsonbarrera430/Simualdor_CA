using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Referencias de Ruedas")]
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider backLeft;
    public WheelCollider backRight;

    public Transform flModel;
    public Transform frModel;
    public Transform blModel;
    public Transform brModel;

    [Header("Configuración de Motor")]
    public float brakeForce = 3000f;
    public float maxSteerAngle = 35f;

    // 0=neutral, 1=primera(MUY lenta), 2=segunda, 3=tercera, 4=reversa
    private float[] gearForce = { 0f, 150f, 700f, 1500f, -600f };

    private int currentGear = 0;
    private float currentSteerAngle;
    private float currentBreakForce;
    private bool isBreaking;
    private float accelerateInput;

    // Update captura botones (no se pierden frames)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton1)) currentGear = 1; // A → Primera
        if (Input.GetKeyDown(KeyCode.JoystickButton3)) currentGear = 2; // X → Segunda
        if (Input.GetKeyDown(KeyCode.JoystickButton2)) currentGear = 3; // Y → Tercera
        if (Input.GetKeyDown(KeyCode.JoystickButton0)) currentGear = 4; // B → Reversa
    }

    // FixedUpdate maneja la física
    private void FixedUpdate()
    {
        HandleInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void HandleInput()
    {
        // ZR acelera, ZL frena (mismo eje)
        float zrRaw = Input.GetAxis("ZR");
        accelerateInput = Mathf.Clamp(zrRaw, 0f, 1f);
        isBreaking = zrRaw < -0.1f;

        // Dirección
        currentSteerAngle = maxSteerAngle * Input.GetAxis("Horizontal");
    }

    private void HandleMotor()
    {
        float torque = accelerateInput * gearForce[currentGear];
        frontLeft.motorTorque  = torque;
        frontRight.motorTorque = torque;

        currentBreakForce = isBreaking ? brakeForce : 0f;
        ApplyBraking();
    }

    private void ApplyBraking()
    {
        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque  = currentBreakForce;
        backLeft.brakeTorque   = currentBreakForce;
        backRight.brakeTorque  = currentBreakForce;
    }

    private void HandleSteering()
    {
        frontLeft.steerAngle  = currentSteerAngle;
        frontRight.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeft,  flModel);
        UpdateSingleWheel(frontRight, frModel);
        UpdateSingleWheel(backLeft,   blModel);
        UpdateSingleWheel(backRight,  brModel);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}