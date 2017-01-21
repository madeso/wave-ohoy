using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncedWaveMover : MonoBehaviour {
	GameObject end;
	float length;

	// Use this for initialization
	void Start () {
		this.end = GameObject.Find("WorldEnd");
		if( this.end == null ) {
			Debug.Log("Unable to get WorldEnd");
		}

		this.length = -1;
		var g = GameObject.Find("MusicPlayer");
		if( g == null ) {
			Debug.Log("Unable to find MusicPlayer");
		}
		else {
			var s = g.GetComponent<AudioSource>();
			if( s == null ) {
				Debug.Log("Failed to find Audio");
			}
			else {
				this.length = s.clip.length;
			}
		}

		if( this.length < 0 ) {
			this.length = 45;
		}
	}
	
	// Update is called once per frame
	void Update () {
		var p = this.transform.localPosition;
		this.transform.localPosition = p - SyncedWaveSpawner.dir * Time.deltaTime;

		if( this.transform.position.x > end.transform.position.x ) {
			var lp = this.transform.localPosition;
			this.transform.localPosition = new Vector3(lp.x-this.length, lp.y, lp.z);
		}
	}
}
