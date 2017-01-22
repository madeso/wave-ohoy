using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steeringWheelRotation : MonoBehaviour {

    Rigidbody2D myShip;

	void Start () {
        myShip = GameObject.FindGameObjectWithTag("ship").GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        transform.Rotate(new Vector3(0f, 1f, 0f), myShip.transform.rotation.z*10f);
	}
}
