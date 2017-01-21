using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// move up and down to hide the several clones look
public class MoveUpAndDown : MonoBehaviour {

	private float position;

	void UpdateTransformation() {
		var p = this.transform.position;
		this.transform.position = new Vector3(p.x, Mathf.Sin(position*Mathf.PI*2)*Globals.Instance.WaveMoveSize/2, p.z);
	}

	void Start() {
		position = Random.value;
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
