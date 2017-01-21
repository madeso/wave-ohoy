using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// spawn new waves
public class WaveSpawner : MonoBehaviour {
	public float HugeWaveFrequency {
		get {
			return Globals.Instance.HugeWaveFrequency;
		}
	}

	public float HugeWaveSize = 10;
	public GameObject HugeWave;
	public float SmallWaveSize = 5;
	public GameObject SmallWave;

	private float WaveSpeed {
		get { return Globals.Instance.Speed; }
	}
	private float sizeLeft = 0;

	private void SpawnNewWave() {
		var p = Random.value;
		if( p < this.HugeWaveFrequency ) {
			this.SpawnHugeWave();
		}
		else {
			this.SpawnSmallWave();
		}
	}

	private void SpawnHugeWave() {
		this.SpawnObject(this.HugeWave);
		this.sizeLeft += this.HugeWaveSize;
	}

	private void SpawnSmallWave() {
		this.SpawnObject(this.SmallWave);
		this.sizeLeft += this.SmallWaveSize;
	}

	private void SpawnObject(GameObject go) {
		if(go == null) {
			return;
		}
		var p = this.transform.position;
		GameObject.Instantiate(go, new Vector3(p.x - this.sizeLeft, p.y, p.z), this.transform.rotation);
	}

	void Start () { }

	void Update () {
		this.sizeLeft -= this.WaveSpeed * Time.deltaTime;
		if( this.sizeLeft < 0 ) {
			this.SpawnNewWave();
		}
	}
}
