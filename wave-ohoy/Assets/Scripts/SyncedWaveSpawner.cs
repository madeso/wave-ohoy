using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncedWaveSpawner : MonoBehaviour {
	public GameObject WaveTemplate;
	public MusicParser Music;
	public MusicParser.Note.Type NoteType;

	public const float Speed = 1;
	public static Vector3 dir = new Vector3(-1, 0, 0);

	void Start() {
		Music.Load();
		foreach(var n in Music.notes) {
			if( n.Event != this.NoteType) continue;
			float distance = n.Time * Speed;
			GameObject.Instantiate(this.WaveTemplate, this.transform.position + dir * distance, Quaternion.identity); 
		}
	}
	
	void Update () {
	}
}
