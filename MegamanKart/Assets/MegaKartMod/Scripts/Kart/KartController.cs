using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartController : MonoBehaviour
{
    [Header("Pneus")]
    [SerializeField] WheelCollider FRWheel;
    [SerializeField] WheelCollider FLWheel;
    [SerializeField] WheelCollider BRWheel;
    [SerializeField] WheelCollider BLWheel;
    WheelCollider[] FWheels;
    WheelCollider[] BWheels;

    [Header("Configura��es")]
    //For�a maxima do carro
    [SerializeField] float motorTorque;

    //Angula��o do carro 
    [SerializeField] float steerAngle;

    [SerializeField] float brakeTorque;
    private void Awake()
    {
        FWheels = new WheelCollider[2];
        FWheels[0] = FRWheel;
        FWheels[1] = FLWheel;

        BWheels = new WheelCollider[2];
        BWheels[0] = BRWheel;
        BWheels[1] = BLWheel;
        GetComponent<BoxCollider>().center = GetComponent<Rigidbody>().centerOfMass;
    }
    private void Update()
    {
        BWheelsMotorTorqueInputAxis(Input.GetAxisRaw("Vertical"));
        FWheelsSteerAngleInputAxis(Input.GetAxisRaw("Horizontal"));
    }
    private void BWheelsMotorTorqueInputAxis(float inputAxis)
    {
        foreach (WheelCollider wheelCollider in BWheels)
        {
            if (inputAxis != 0) wheelCollider.motorTorque = inputAxis * motorTorque;
        }
    }
    private void FWheelsSteerAngleInputAxis(float inputAxis)
    {
        foreach (WheelCollider wheelCollider in FWheels)
        {
            wheelCollider.steerAngle = inputAxis * steerAngle;
            //wheelCollider.transform.rotation = Quaternion.Euler(new Vector3(0, -90 + (steerAngle * inputAxis), 0));
        }
    }
}