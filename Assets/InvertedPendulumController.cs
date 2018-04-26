using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvertedPendulumController : MonoBehaviour {

    public PID tiltPID = new PID();

    public GameObject leftSlider, rightSlider;

    public Accelerometer accel;
    public Gyroscope gyro;

    public float beta;

	void FixedUpdate () {
        float tiltAccel = Mathf.Atan2(accel.Accel.y, accel.Accel.z);
        float tiltGyro = gyro.Angle;
        float tiltAngle = beta * tiltGyro + (1 - beta) * tiltAccel;
        //tiltAngle = 10 * (Mathf.Abs(tiltAngle) + 1) * Mathf.Sign(tiltAngle);
        
        //float setpoint = encoderPID.PushError(Time.deltaTime, (le + re) / 2);

        //ma.Push(setpoint);
        float tiltError = Mathf.Sign(tiltAngle) * Mathf.Pow(Mathf.Abs(tiltAngle), 1.2f);
        Debug.Log(tiltError);
        float sliderPos = tiltPID.PushError(Time.deltaTime, tiltError);

        leftSlider.transform.Translate(0, 0, sliderPos);
        rightSlider.transform.Translate(0, 0, sliderPos);

        GameObject.FindWithTag("TiltText").GetComponent<Text>().text = "" + tiltAngle;
        GameObject.FindWithTag("RightMotorText").GetComponent<Text>().text = "" + sliderPos;
        GameObject.FindWithTag("LeftMotorText").GetComponent<Text>().text = "" + sliderPos;
	}
}
