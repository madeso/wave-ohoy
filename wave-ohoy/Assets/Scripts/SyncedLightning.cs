﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncedLightning : MonoBehaviour {
	public MusicParser Music;
	public MusicParser.Note.Type Note;

	private Light light;

    AudioSource ass;

    public Camera camera;
    bool mode = false;

	void Start () {
		this.light = this.GetComponent<Light>();
		if( this.light == null ) {
			Debug.Log("Failed to find the light");
		}

        ass = GetComponent<AudioSource>();
	}

	float effect = 0;
	
	// Update is called once per frame
	void Update () {
		this.effect -= Time.deltaTime;
		if( this.effect < 0 ) this.effect = 0;

		if( this.Music != null ) {
			foreach(var n in this.Music.CurrentNotes) {
				if( n.Event == this.Note) {
					this.effect = 1;

                    ass.Play();
				}
			}
		}

		if( this.light != null ) {
			this.light.enabled = this.effect > 0.0f;
			this.light.intensity = this.effect;
		}
	}
}
