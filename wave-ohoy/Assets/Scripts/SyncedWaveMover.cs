using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncedWaveMover : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		var p = this.transform.localPosition;
		this.transform.localPosition = p - SyncedWaveSpawner.dir * Time.deltaTime;
	}
}
