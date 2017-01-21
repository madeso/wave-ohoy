using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// kills the game object if it goes outside the view
public class KillOutside : MonoBehaviour {
	Renderer r;

	void Start() {
		r = GetComponentInChildren<Renderer>();
		if( r == null ) {
			Debug.LogError("KillOutside: failed to find renderer");
		}
	}
	void Update () {
		if( r == null ) return;
		if( r.isVisible==false )
		{
			// Debug.Log("KillOutside: Destroyed game object");
			GameObject.Destroy(this.gameObject);
		}
	}
}
