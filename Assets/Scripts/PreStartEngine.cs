using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreStartEngine : MonoBehaviour {

    private float beginTime;

	// Use this for initialization
	void Start () {
        ((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();

        beginTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("JoystickA") || (Time.time - beginTime) > 35.0f)
            SceneManager.LoadScene("Start Scene");
    }
}
