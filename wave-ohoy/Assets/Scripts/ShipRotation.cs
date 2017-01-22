using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotation : MonoBehaviour {

    Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        rb.AddTorque(-transform.rotation.z*750f, ForceMode2D.Force);

        rb.transform.position += new Vector3(0f, (-Mathf.Abs(transform.rotation.z)) / 90f, 0f) * Time.deltaTime;
	}

    public void Rock()
    {
        rb.AddTorque(-250f, ForceMode2D.Impulse);
    }
}
