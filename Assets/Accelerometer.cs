using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour {

    public float noise = 0;

    [SerializeField]
    private Vector3 accel;
    public Vector3 Accel {
        get {
            return accel;
        }
        private set {
            this.accel = value;
        }
    }
    private Vector3 lastVelocity;

	// Use this for initialization
	void Awake() {
		Time.timeScale = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 adjustedGravity = Quaternion.Inverse(transform.rotation) * Physics.gravity;
        Vector3 v = GetComponent<Rigidbody>().velocity;
        Vector3 accelBase = (v - lastVelocity) / Time.deltaTime + adjustedGravity;
        accel = accelBase + Random.onUnitSphere * noise;
        lastVelocity = v;
	}
}
