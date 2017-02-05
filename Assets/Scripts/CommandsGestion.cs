using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CommandsGestion : MonoBehaviour {

    public Canvas textCanvas;
    public Canvas buttonsCanvas;

	// Use this for initialization
	void Start () {
        textCanvas.enabled = true;
        buttonsCanvas.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("JoystickA"))
        {
            textCanvas.enabled = !textCanvas.enabled;
            buttonsCanvas.enabled = !buttonsCanvas.enabled;
        }

        if (Input.GetButtonDown("JoystickB"))
            SceneManager.LoadScene("Start Scene");
    }
}
