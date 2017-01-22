using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAssignment : MonoBehaviour {

	public GameObject pirate1;
	public GameObject pirate2;
	public GameObject pirate3;
	public GameObject pirate4;

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void AssignControllers(int p1, int p2, int p3, int p4)
    {
        pirate1.GetComponent<Controller>().myPlayerIndex = p1;
        pirate2.GetComponent<Controller>().myPlayerIndex = p2;
        pirate3.GetComponent<Controller>().myPlayerIndex = p3;
        pirate4.GetComponent<Controller>().myPlayerIndex = p4;
    }
}
