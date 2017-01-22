using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class theScript : MonoBehaviour {

    public GameObject pirate1;
    public GameObject pirate2;
    public GameObject pirate3;
    public GameObject pirate4;

    public void Drown()
    {
        if(
            pirate1.GetComponent<Controller>().isDead &&
            pirate2.GetComponent<Controller>().isDead &&
            pirate3.GetComponent<Controller>().isDead &&
            pirate4.GetComponent<Controller>().isDead
        )
        {
            SceneManager.LoadScene(0);
        }
    }
}
