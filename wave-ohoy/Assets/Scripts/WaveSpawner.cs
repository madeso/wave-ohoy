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
		this.sizeLeft += this.HugeWaveSize;
		this.SpawnObject(this.HugeWave);
	}

	private void SpawnSmallWave() {
		this.sizeLeft += this.SmallWaveSize;
		this.SpawnObject(this.SmallWave);
	}

	private void SpawnObject(GameObject go) {
		if(go == null) {
			return;
		}
		// todo: change position based on sizeLeft
		GameObject.Instantiate(go, this.transform.position, this.transform.rotation);
	}

	void Start () { }

	void Update () {
		this.sizeLeft -= this.WaveSpeed * Time.deltaTime;
		if( this.sizeLeft < 0 ) {
			this.SpawnNewWave();
		}
	}
}
