using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartEngine : MonoBehaviour {

    public string GameSceneSolo;
    public string GameSceneVersus;
    public string CommandsScene;

    public Button buttonStartSolo;
    public Button buttonStart1v1;
    public Button buttonHTP;

    public Color unselectedColorTextButton;
    public Color selectedColorButton;

    private string ActiveNextScene;

    private bool firstTouch;

    // Use this for initialization
    void Start()
    {
        firstTouch = true;

        ActiveNextScene = GameSceneSolo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("JoystickA"))
            SceneManager.LoadScene(ActiveNextScene);

        if (Input.GetAxisRaw("VerticalCross") == 1.0f  && firstTouch)
        {
            firstTouch = false;

            if (ActiveNextScene == GameSceneSolo)
                ActiveNextScene = CommandsScene;
            else if (ActiveNextScene == GameSceneVersus)
                ActiveNextScene = GameSceneSolo;
            else
                ActiveNextScene = GameSceneVersus;

            Debug.Log("Active : " + ActiveNextScene);
        }

        if (Input.GetAxisRaw("VerticalCross") == -1.0f && firstTouch)
        {
            firstTouch = false;

            if (ActiveNextScene == GameSceneSolo)
                ActiveNextScene = GameSceneVersus;
            else if (ActiveNextScene == GameSceneVersus)
                ActiveNextScene = CommandsScene;
            else
                ActiveNextScene = GameSceneSolo;

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
        if (ActiveNextScene == GameSceneSolo)
        {
            buttonStartSolo.image.color = selectedColorButton;
            buttonHTP.image.color = Color.white;
            buttonStart1v1.image.color = Color.white;

            buttonStartSolo.GetComponent<RectTransform>().sizeDelta = new Vector2(240.0f, 50.0f);
            buttonHTP.GetComponent<RectTransform>().sizeDelta = new Vector2(220.0f, 50.0f);
            buttonStart1v1.GetComponent<RectTransform>().sizeDelta = new Vector2(220.0f, 50.0f);

            buttonStartSolo.GetComponentInChildren<Text>().color = Color.white;
            buttonHTP.GetComponentInChildren<Text>().color = unselectedColorTextButton;
            buttonStart1v1.GetComponentInChildren<Text>().color = unselectedColorTextButton;
        }
        else if (ActiveNextScene == CommandsScene)
        {
            buttonStartSolo.image.color = Color.white;
            buttonHTP.image.color = selectedColorButton;
            buttonStart1v1.image.color = Color.white;

            buttonStartSolo.GetComponent<RectTransform>().sizeDelta = new Vector2(220.0f, 50.0f);
            buttonHTP.GetComponent<RectTransform>().sizeDelta = new Vector2(240.0f, 50.0f);
            buttonStart1v1.GetComponent<RectTransform>().sizeDelta = new Vector2(220.0f, 50.0f);

            buttonStartSolo.GetComponentInChildren<Text>().color = unselectedColorTextButton;
            buttonHTP.GetComponentInChildren<Text>().color = Color.white;
            buttonStart1v1.GetComponentInChildren<Text>().color = unselectedColorTextButton;
        }
        else
        {
            buttonStartSolo.image.color = Color.white;
            buttonHTP.image.color = Color.white;
            buttonStart1v1.image.color = selectedColorButton;

            buttonStartSolo.GetComponent<RectTransform>().sizeDelta = new Vector2(220.0f, 50.0f);
            buttonHTP.GetComponent<RectTransform>().sizeDelta = new Vector2(220.0f, 50.0f);
            buttonStart1v1.GetComponent<RectTransform>().sizeDelta = new Vector2(240.0f, 50.0f);

            buttonStartSolo.GetComponentInChildren<Text>().color = unselectedColorTextButton;
            buttonHTP.GetComponentInChildren<Text>().color = unselectedColorTextButton;
            buttonStart1v1.GetComponentInChildren<Text>().color = Color.white;
        }
    }
}