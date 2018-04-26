using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PID {

    public float p, i, d;

    [System.NonSerialized]
    private float sum = 0f, lastError = 0f;

	public float PushError(float dt, float e) {
        sum += e * dt;
        float delta = (e - lastError) / dt;
        float ret = p * e + i * sum + d * delta;
        lastError = e;
        return ret;
    }
}
