using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelSpawn : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = new Vector3(0f, Random.value * 50f + 50f, 0f);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;
    }

}
