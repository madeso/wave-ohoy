using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotation : MonoBehaviour {

    Rigidbody2D rb;
    public Camera camera;

    private Vector3 startPos;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        startPos = camera.transform.position;
	}
	
	void Update () {
        rb.AddTorque(-transform.rotation.z*750f, ForceMode2D.Force);

        rb.transform.position += new Vector3(0f, (-Mathf.Abs(transform.rotation.z)) / 90f, 0f) * Time.deltaTime;

        camera.transform.position = startPos - new Vector3(transform.rotation.z, 0f, 0f)*2.5f;
	}

    public void Rock()
    {
        rb.AddTorque(-250f, ForceMode2D.Impulse);
    }
}
