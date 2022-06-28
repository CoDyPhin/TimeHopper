using UnityEngine;
using UnityEngine.UI;

public class HubPause : MonoBehaviour
{
    public Button exitButton;
    public Button resumeButton;
    public Button settingsButton;
    public Button instructionsButton;
    public Button backSettingsButton;
    public Button backInstructionsButton;

    public GameObject core;
    public GameObject settingsCore;
    public GameObject instructionsCore;

    public Canvas canvas;
    private StarterAssets.ThirdPersonController player;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssets.ThirdPersonController>();

        exitButton.onClick.AddListener(OnClickExit);
        resumeButton.onClick.AddListener(OnClickResume);
        settingsButton.onClick.AddListener(OnClickSettings);
        instructionsButton.onClick.AddListener(OnClickInstructions);
        backSettingsButton.onClick.AddListener(OnClickBackSettings);
        backInstructionsButton.onClick.AddListener(OnClickBackInstructions);

        core.SetActive(true); // Main is the default core
        settingsCore.SetActive(false);
        instructionsCore.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Toggle(!canvas.enabled);
        }
    }

    public void Toggle(bool value)
    {
        canvas.enabled = value;
        if (value)
        {
            Time.timeScale = 0f;
            player.SwitchInputToUI();
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;
            player.SwitchInputToPlayer();
            Cursor.lockState = CursorLockMode.Locked;
            core.SetActive(true); // Main is the default core
            settingsCore.SetActive(false);
            instructionsCore.SetActive(false);
        }
    }

    void OnClickExit()
    {
        canvas.enabled = false;
        Time.timeScale = 1f;
        ExitApplication();
    }

    void OnClickResume()
    {
        canvas.enabled = false;
        Time.timeScale = 1f;
        player.SwitchInputToPlayer();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnClickSettings()
    {
        core.SetActive(false);
        settingsCore.SetActive(true);
        instructionsCore.SetActive(false);
    }

    void OnClickInstructions()
    {
        core.SetActive(false);
        settingsCore.SetActive(false);
        instructionsCore.SetActive(true);
    }

    void OnClickBackSettings()
    {
        core.SetActive(true);
        settingsCore.SetActive(false);
        instructionsCore.SetActive(false);
    }

    void OnClickBackInstructions()
    {
        core.SetActive(true);
        settingsCore.SetActive(false);
        instructionsCore.SetActive(false);
    }

    void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
