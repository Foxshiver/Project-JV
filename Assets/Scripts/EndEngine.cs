using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndEngine : MonoBehaviour {

    public string GameScene;
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


    // Use this for initialization
    void Start()
    {
        if(PersistentData.nbOfWaves >= 10)
        {
            winningEnd.SetActive(true);
            losingEnd.SetActive(false);
            scoreMessage.text = "You have defend your farm from " + PersistentData.nbOfWaves + " attacks.\nYou have survive " + Mathf.Round(PersistentData.TimeAlive) + " secs";
        }
        else
        {
            losingEnd.SetActive(true);
            winningEnd.SetActive(false);
            scoreMessage.text = "You died after " + PersistentData.nbOfWaves + " attacks.\nYou have survive " + Mathf.Round(PersistentData.TimeAlive) + " secs";
        }

        firstTouch = true;

        ActiveNextScene = GameScene;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("JoystickA"))
        {
            if (ActiveNextScene == GameScene)
                SceneManager.LoadScene(ActiveNextScene);
            else
                Application.Quit();
        }
            

        if ((Input.GetAxisRaw("VerticalCross") == 1.0f || Input.GetAxisRaw("VerticalCross") == -1.0f) && firstTouch)
        {
            firstTouch = false;

            if (ActiveNextScene == GameScene)
                ActiveNextScene = CommandsScene;
            else
                ActiveNextScene = GameScene;

            Debug.Log("Active : " + ActiveNextScene);
        }

        if (Input.GetAxisRaw("VerticalCross") == 0.0f)
        {
            firstTouch = true;
        }

        ShowActiveSelection();
    }

    public void ShowActiveSelection()
    {
        if (ActiveNextScene == GameScene)
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