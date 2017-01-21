﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicParser : MonoBehaviour {
	public TextAsset Data;

	public class Note {
		public enum Type {
			cymbal, thunder, wind, boom, note
		}

		public float Time;
		public Type Event;
	}
	public List<Note> notes = new List<Note>();
	public List<Note> CurrentNotes = new List<Note>();
	public float elapsedTime = 0;

	private bool IsLoaded = false;

	public void Load() {
		if( this.IsLoaded ) return;
		this.IsLoaded = true;

		// todo: replace with proper json parser
		var d = this.Data.text.Split("\n".ToCharArray());
		for(int i=2; i<d.Length;) {
			var s = d[i].Substring(0, d[i].Length-1);
			var time = float.Parse(s);
			var t = d[i+3].Trim();
			var e = t.Substring(1, t.Length-2);
			Note.Type ev = Note.Type.cymbal;
			try {
				ev = (Note.Type) Enum.Parse(typeof(Note.Type), e);
			}
			catch(Exception) {
				Debug.LogError("Tried to parse enum " + e);
			}
			notes.Add(new Note { Time=time, Event=ev} );
			i += 6;
		}
	}

	// Use this for initialization
	void Start () {
		this.Load();
	}
	
	// Update is called once per frame
	void Update () {
		float lastTime = elapsedTime;
		elapsedTime += Time.deltaTime;

		this.CurrentNotes.Clear();
		foreach(var n in this.notes) {
			if( n.Time > lastTime && n.Time < elapsedTime ) {
				this.CurrentNotes.Add(n);
			}
		}
	}
}
