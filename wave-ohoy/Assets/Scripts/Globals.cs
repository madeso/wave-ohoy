using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// todo: rename to tweaks?
public class Globals : MonoBehaviour {
	private static Globals globals = null;
	public static Globals Instance {
		get {
			if( globals == null ) {
				var g = GameObject.Find("Globals");
				if( g != null ) {
					globals = g.GetComponent<Globals>();
					// todo: display error and fail
				}
				else {
					// todo: display error and fail
				}
			}
			return globals;
		}
	}

	// the wave speed
	public float Speed = 1;

	// direction of 'left'
	public Vector3 Left = new Vector3(1,0,0);

	// todo: use visibility range check instead
	// remove waves if they go beyond this range
	public float WaveKillRange = 10;

	// how much the waves fluctuate
	public float WaveMoveSize = 0.1f;

	// how often we spawn a huge wave, 0-1
	public float HugeWaveFrequency = 0.1f;
}
