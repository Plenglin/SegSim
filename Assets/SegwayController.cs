using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SegwayController : MonoBehaviour {

    public PID tiltPID = new PID();
    public PID yawPID = new PID();
    public PID velPID = new PID();

    public Motor leftMotor, rightMotor;

    public Accelerometer accel;
    public Gyroscope pitch, yaw;

    public Rigidbody trackVelocity;

    public int maLength;
    public float beta;
    public float maxAngleSetpoint;
    private MovingAverage ma;

    private float lastZ;

    void Start() {
        ma = new MovingAverage(maLength);
        lastZ = trackVelocity.position.z;
    }
	
	void FixedUpdate () {
        float pitchAccel = Mathf.Atan2(accel.Accel.y, accel.Accel.z);
        float pitchGyro = pitch.Angle;
        float pitchAngle = beta * pitchGyro + (1 - beta) * pitchAccel;

        float errorVel = (trackVelocity.position.z - lastZ) / Time.fixedDeltaTime;
        lastZ = trackVelocity.position.z;
        
        float setpointPitch = velPID.PushError(Time.fixedDeltaTime, -errorVel);
        //float setpointPitch = Mathf.PI / 32;
        //float setpointPitch = 0;
        
        float errorPitch = pitchAngle - Mathf.Clamp(setpointPitch, -maxAngleSetpoint, maxAngleSetpoint);

        //float pitchFeedForward = -Physics.gravity.y * Mathf.Tan(pitchAngle);
        //Debug.Log(pitchFeedForward);
        
        float powerYaw = yawPID.PushError(Time.fixedDeltaTime, yaw.Angle);
        float powerPitch = tiltPID.PushError(Time.fixedDeltaTime, errorPitch);

        leftMotor.power = powerPitch - powerYaw;
        rightMotor.power = powerPitch + powerYaw;

        GameObject.FindWithTag("TiltText").GetComponent<Text>().text = "" + pitchAngle;
        GameObject.FindWithTag("RightMotorText").GetComponent<Text>().text = "" + rightMotor.power;
        GameObject.FindWithTag("LeftMotorText").GetComponent<Text>().text = "" + leftMotor.power;
	}
}
