using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLogic : MonoBehaviour {
	CloudTweaker tweaks;
	Vector3 scale;

	MusicParser music;

	public float LocalThumpOffset = 0;

	void Start () {
		this.tweaks = GameObject.Find("CloudTweaker").GetComponent<CloudTweaker>();
		this.music = GameObject.Find("MusicParser").GetComponent<MusicParser>();
		this.scale = this.transform.localScale;
	}

	private float p = 0;
	private float thump = 0;
	private static float wrap01(float i) {
		var f = i;
		while(f > 1) f-=1;
		return f;
	}

	void Update () {
		var o = this.tweaks.Offset + this.LocalThumpOffset + 0.5f;
		var last = wrap01(this.p+o);
		this.p = wrap01(this.p+Time.deltaTime * (1/this.tweaks.TimeBetweenThumps) );
		var current = wrap01(this.p+o);

		foreach(var n in music.CurrentNotes) {
			if( n.Event != MusicParser.Note.Type.boom) {
				continue;
			}
			thump = 1;
		}

		if( last < 0.5f && current >= 0.5f ) {
			// thump = 1;
		}

		if( thump > 0 ) {
			this.thump -= Time.deltaTime * (1/tweaks.ThumpTime);
			if( this.thump < 0 ) this.thump = 0;
		}

		// todo: interpolate thumping
		var thump_value = this.thump*2;
		var interpolated_thump_value = Mathf.Abs(Mathf.Sin( thump_value*Mathf.PI )) * this.tweaks.ThumpScale;
		this.transform.localScale = this.scale * (1+interpolated_thump_value);
	}
}
