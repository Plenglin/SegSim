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
    public float targetVelocity;

    public int maLength;
    public float beta;
    public float maxAngleSetpoint;
    public float maxVelocitySetpoint;
    private MovingAverage ma;
    private float angVelFeed;
    private float lastZ;

    void Start() {
        ma = new MovingAverage(maLength);
        lastZ = trackVelocity.position.z;
    }
	
	void FixedUpdate () {
        float dt = Time.fixedDeltaTime;

        float pitchAccel = Mathf.Atan2(accel.Accel.y, accel.Accel.z);
        float pitchGyro = pitch.Angle;
        float pitchAngle = beta * pitchGyro + (1 - beta) * pitchAccel;

        float errorVel = (trackVelocity.position.z - lastZ) / dt;
        lastZ = trackVelocity.position.z;
        
        float setpointPitch = velPID.PushError(dt, -errorVel - Mathf.Clamp(targetVelocity, -maxVelocitySetpoint, maxVelocitySetpoint));
        //float setpointPitch = Mathf.PI / 32;
        //float setpointPitch = 0;
        
        float errorPitch = pitchAngle - Mathf.Clamp(setpointPitch, -maxAngleSetpoint, maxAngleSetpoint);

        angVelFeed += -Physics.gravity.y * Mathf.Tan(pitchAngle) * dt;
        //Debug.Log(pitchFeedForward);
        
        float powerYaw = yawPID.PushError(dt, yaw.Angle);
        float powerPitch = tiltPID.PushError(dt, errorPitch, angVelFeed);

        leftMotor.power = powerPitch - powerYaw;
        rightMotor.power = powerPitch + powerYaw;

        GameObject.FindWithTag("TiltText").GetComponent<Text>().text = "" + pitchAngle;
        GameObject.FindWithTag("RightMotorText").GetComponent<Text>().text = "" + rightMotor.power;
        GameObject.FindWithTag("LeftMotorText").GetComponent<Text>().text = "" + leftMotor.power;
	}
}
