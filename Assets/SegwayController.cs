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

    public int maLength;
    public float beta;
    private MovingAverage ma;

    void Start() {
        ma = new MovingAverage(maLength);
    }
	
	void FixedUpdate () {
        float pitchAccel = Mathf.Atan2(accel.Accel.y, accel.Accel.z);
        float pitchGyro = pitch.Angle;
        float pitchAngle = beta * pitchGyro + (1 - beta) * pitchAccel;
        
        float pitchError = pitchAngle;

        float powerYaw = yawPID.PushError(Time.fixedDeltaTime, yaw.Angle);
        float powerPitch = tiltPID.PushError(Time.fixedDeltaTime, pitchError);

        leftMotor.power = powerPitch - powerYaw;
        rightMotor.power = powerPitch + powerYaw;

        GameObject.FindWithTag("TiltText").GetComponent<Text>().text = "" + pitchAngle;
        GameObject.FindWithTag("RightMotorText").GetComponent<Text>().text = "" + rightMotor.power;
        GameObject.FindWithTag("LeftMotorText").GetComponent<Text>().text = "" + leftMotor.power;
	}
}
