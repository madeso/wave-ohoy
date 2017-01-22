using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// kills the game object if it goes outside the view
public class KillOutside : MonoBehaviour {
	private Renderer r;
	private int frameIgnores = 0;

	void Start() {
		r = GetComponentInChildren<Renderer>();
		if( r == null ) {
			Debug.LogError("KillOutside: failed to find renderer");
		}
	}
	void Update () {
		// Update might be called before isVisible is updated, and hence we remove the GO before we have had
		// the time to render it, so let's just keep the GO alive for a few updates before we remove it
		if( this.frameIgnores < 3 ) {
			this.frameIgnores++;
			return;
		}
		if( r == null ) return;
		if( r.isVisible==false )
		{
			// Debug.Log("KillOutside: Destroyed game object");
			GameObject.Destroy(this.gameObject);
		}
	}
}
