using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour {

    public float noise = 0;

    public Vector3 axis;

    [SerializeField]
    private float angle;
    public float Angle {
        get {
            return angle;
        }
        private set {
            this.angle = value;
        }
    }

    [SerializeField]
    private float velocity;
    public float Velocity {
        get {
            return velocity;
        }
        private set {
            this.velocity = value;
        }
    }

    // Use this for initialization
    void Start() {
		
	}
	
	// Update is called once per frame
	void Update () {
        //velocity = Vector3.Dot(GetComponent<Rigidbody>().angularVelocity, transform.position + axis.normalized) + noise * (2 * Random.value - 1);
        //angle += Time.deltaTime * velocity;
        float a;
        Vector3 v;
        transform.rotation.ToAngleAxis(out a, out v);
        a *= Vector3.Dot(transform.rotation * axis, v);
        angle = a * Mathf.PI / 180;
	}
}
