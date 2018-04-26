using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour {

	public float power;
	public float maxTorque, maxAngularVelocity;
	public Vector3 axis;

	private Rigidbody rb;

	void Start () {
		this.rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate() {
		float realPower = Mathf.Clamp(Mathf.Abs(power), 0, 1);
		float absTorque = (realPower * maxTorque * (maxAngularVelocity - rb.angularVelocity.magnitude)) / maxAngularVelocity;
		float realTorque = Mathf.Sign(power) * absTorque;
		Vector3 outTorque = axis * realTorque;
		Debug.Log(absTorque);
		Debug.DrawLine(transform.position, transform.position + transform.rotation * outTorque);
		rb.AddRelativeTorque(outTorque);
	}
}
