using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// move object in a single direction based on the global speed
public class WaveMover : MonoBehaviour {
	void Start () {
	}

	void Update () {
		this.transform.position = this.transform.position + Globals.Instance.Left * Time.deltaTime * Globals.Instance.Speed;
	}
}
