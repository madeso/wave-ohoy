using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShip : MonoBehaviour {

    Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        rb.AddTorque(-Input.GetAxis("Horizontal4")*1000f, ForceMode2D.Force);
	}
}
