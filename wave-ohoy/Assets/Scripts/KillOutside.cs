using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// kills the game object if it goes outside the view
public class KillOutside : MonoBehaviour {
	void Update () {
		// todo: change so that we detect camera visibility and remove it instead of the crappy "range" we have now
		if( this.transform.position.x > Globals.Instance.WaveKillRange ) {
			GameObject.Destroy(this.gameObject);
		}
	}
}
