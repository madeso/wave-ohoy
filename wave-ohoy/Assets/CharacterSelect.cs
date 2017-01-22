using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CharacterSelect : MonoBehaviour {

	public GameObject pirate1;
	public GameObject pirate2;
	public GameObject pirate3;
	public GameObject pirate4;

	public GameObject arrow;

	public int selection1;
	public int selection2;
	public int selection3;
	public int selection4;

    private bool selected1 = false;
    private bool selected2 = false;
    private bool selected3 = false;
    private bool selected4 = false;

    private bool loaded = false;
    private bool loaded1 = false;

    AudioSource ass;

	void Start () {
		selection1 = selection2 = selection3 = selection4 = 0;
		arrow.transform.position = pirate1.transform.position + new Vector3(0f, 1f, 0f);
        ass = GetComponent<AudioSource>();
	}
	
	void Update () {
        if(loaded && !loaded1)
        {
            GameObject.Find("ControllerDistributer").GetComponent<ControllerAssignment>().AssignControllers(selection1, selection2, selection3, selection4);
            loaded1 = true;
        }

        if(selected1 && selected2 && selected3 && selected4)
        {
            if (
                (
                    selection1 != 0 &&
                    selection2 != 0 &&
                    selection3 != 0 &&
                    selection4 != 0
                ) && !loaded
            )
            {
                GameObject.Destroy(GameObject.Find("selectStuff"));
                SceneManager.LoadScene(1, LoadSceneMode.Additive);
                loaded = true;
            }
        }
        else
        {
            SelectPlayer();
        }
	}

	void SelectPlayer()
	{
        int selection = 0;
        if(Input.GetButtonDown("Jump1") && !selected1)
        {
            selection = 1;
        }
        else if(Input.GetButtonDown("Jump2") && !selected2)
        {
            selection = 2;
        }
        else if(Input.GetButtonDown("Jump3") && !selected3)
        {
            selection = 3;
        }
        else if(Input.GetButtonDown("Jump4") && !selected4)
        {
            selection = 4;
        }

        if(selection != 0)
        {
            if(selection1 == 0)
            {
                selection1 = selection;
        		arrow.transform.position = pirate2.transform.position + new Vector3(0f, 1.5f, 0f);
            }
            else if(selection2 == 0)
            {
                selection2 = selection;
        		arrow.transform.position = pirate3.transform.position + new Vector3(0f, 1.5f, 0f);
            }
            else if(selection3 == 0)
            {
                selection3 = selection;
        		arrow.transform.position = pirate4.transform.position + new Vector3(0f, 2f, 0f);
            }
            else if(selection4 == 0)
            {
                selection4 = selection;
            }

            ass.Play();

            if(selection == 1)
            {
                selected1 = true;
            }
            else if(selection == 2)
            {
                selected2 = true;
            }
            else if(selection == 3)
            {
                selected3 = true;
            }
            else if(selection == 4)
            {
                selected4 = true;
            }
        }
	}
}
