using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveRockingShip : MonoBehaviour {
	private SyncedWaveSpawner ws;

	void Start () {
		this.ws = this.GetComponent<SyncedWaveSpawner>();
		if( this.ws == null ) {
			Debug.Log("Failed to find synced wave spawner");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if( this.ws == null ) return;
		if( this.ws.Music == null ) return;
		foreach(var n in this.ws.Music.CurrentNotes) {
			//Debug.Log(n.Event);
			if( n.Event != this.ws.NoteType ) continue;
			this.RockTheBoat();
		}
	}

	void RockTheBoat() {
		// a wave has hit the boat, rock it!
		Debug.Log("Rocking the boat");
	}
}
