using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndEngine : MonoBehaviour {

    public string StartScene;
    public string CommandsScene;

    public Button buttonStart;
    public Button buttonExit;

    public Color unselectedColorTextButton;
    public Color selectedColorButton;

    private string ActiveNextScene;

    private bool firstTouch;

    public Text scoreMessage;

    public GameObject winningEnd;
    public GameObject losingEnd;
    public GameObject endFor1v1;

    public AudioSource endSound;


    // Use this for initialization
    void Start()
    {
        if(PersistentData.nbPlayer == 1)
        {
            if (PersistentData.nbOfWaves >= 10)
            {
                endSound.pitch = 1.2f;

                winningEnd.SetActive(true);
                losingEnd.SetActive(false);
                endFor1v1.SetActive(false);
                scoreMessage.text = "You have defend your farm from " + PersistentData.nbOfWaves + " attacks.\nYou have survive " + Mathf.Round(PersistentData.TimeAlive) + " secs";
            }
            else
            {
                endSound.pitch = 0.7f;

                losingEnd.SetActive(true);
                winningEnd.SetActive(false);
                endFor1v1.SetActive(false);
                scoreMessage.text = "You died after " + PersistentData.nbOfWaves + " attacks.\nYou have survive " + Mathf.Round(PersistentData.TimeAlive) + " secs";
            }
        }
        else
        {
            endSound.pitch = 1.2f;

            endFor1v1.SetActive(true);
            winningEnd.SetActive(false);
            losingEnd.SetActive(false);
            scoreMessage.text = "Player " + PersistentData.winner + " destroy the farm first !";
        }


        firstTouch = true;

        ActiveNextScene = StartScene;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("JoystickA"))
        {
            if (ActiveNextScene == StartScene)
                SceneManager.LoadScene(ActiveNextScene);
            else
                Application.Quit();
        }
            

        if ((Input.GetAxisRaw("VerticalCross") == 1.0f || Input.GetAxisRaw("VerticalCross") == -1.0f) && firstTouch)
        {
            firstTouch = false;

            if (ActiveNextScene == StartScene)
                ActiveNextScene = CommandsScene;
            else
                ActiveNextScene = StartScene;
        }

        if (Input.GetAxisRaw("VerticalCross") == 0.0f)
        {
            firstTouch = true;
        }

        ShowActiveSelection();
    }

    public void ShowActiveSelection()
    {
        if (ActiveNextScene == StartScene)
        {
            buttonStart.image.color = selectedColorButton;
            buttonExit.image.color = Color.white;

            buttonStart.GetComponent<RectTransform>().sizeDelta = new Vector2(240.0f, 50.0f);
            buttonExit.GetComponent<RectTransform>().sizeDelta = new Vector2(220.0f, 50.0f);

            buttonStart.GetComponentInChildren<Text>().color = Color.white;
            buttonExit.GetComponentInChildren<Text>().color = unselectedColorTextButton;
        }
        else
        {
            buttonStart.image.color = Color.white;
            buttonExit.image.color = selectedColorButton;

            buttonStart.GetComponent<RectTransform>().sizeDelta = new Vector2(220.0f, 50.0f);
            buttonExit.GetComponent<RectTransform>().sizeDelta = new Vector2(240.0f, 50.0f);

            buttonStart.GetComponentInChildren<Text>().color = unselectedColorTextButton;
            buttonExit.GetComponentInChildren<Text>().color = Color.white;
        }
    }
}