using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Motor : MonoBehaviour {

	public float power;
	public float maxTorque, maxAngularVelocity;
	public Vector3 axis;
	public Text logTo;

	private Rigidbody rb;

	void Start () {
		this.rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate() {
		float realPower = Mathf.Clamp(Mathf.Abs(power), 0, 1);
		float absTorque = (realPower * maxTorque * (maxAngularVelocity - rb.angularVelocity.magnitude)) / maxAngularVelocity;
		float realTorque = Mathf.Sign(power) * absTorque;
		Vector3 outTorque = axis * realTorque;
		Debug.DrawLine(transform.position, transform.position + transform.rotation * outTorque);
		rb.AddRelativeTorque(outTorque);
		if (logTo != null) {
			logTo.text = rb.angularVelocity.magnitude.ToString();
		}
	}
}
