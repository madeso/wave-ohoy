using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUpAndDown : MonoBehaviour {
	public float position = 0.0f;
	public float WaveMoveSize = 0.1f;

	void UpdateTransformation() {
		var p = this.transform.localPosition;
		this.transform.localPosition = new Vector3(p.x + Mathf.Cos(Time.time+p.y*5f)*0.01f, p.y+Mathf.Sin(position*Mathf.PI*2)*WaveMoveSize/2, p.z);
	}

	void Start() {
		this.UpdateTransformation();
	}

	void Update () {
		position += Time.deltaTime;
		while(position >1) {
			position -= 1;
		}
		this.UpdateTransformation();
	}
}
