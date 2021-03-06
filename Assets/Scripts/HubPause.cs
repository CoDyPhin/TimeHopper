using TMPro;
using System;
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

    public Slider sliderSFX;
    public Slider sliderMusic;
    public TextMeshProUGUI textSFX;
    public TextMeshProUGUI textMusic;

    public Canvas canvas;
    private StarterAssets.ThirdPersonController player;

    private FMOD.Studio.Bus sfxBus;
    private FMOD.Studio.Bus musicBus;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;

        player = GameObject.Find("PlayerArmature").GetComponent<StarterAssets.ThirdPersonController>();

        exitButton.onClick.AddListener(OnClickExit);
        resumeButton.onClick.AddListener(OnClickResume);
        settingsButton.onClick.AddListener(OnClickSettings);
        instructionsButton.onClick.AddListener(OnClickInstructions);
        backSettingsButton.onClick.AddListener(OnClickBackSettings);
        backInstructionsButton.onClick.AddListener(OnClickBackInstructions);

        core.SetActive(true); // Main is the default core
        settingsCore.SetActive(false);
        instructionsCore.SetActive(false);

        musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/SFX");

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            sliderSFX.value = PlayerPrefs.GetFloat("sfxVolume");
            sfxBus.setVolume(sliderSFX.value);
        }
        else
        {
            sliderSFX.value = 0.7f;
            PlayerPrefs.SetFloat("sfxVolume", sliderSFX.value);
            sfxBus.setVolume(sliderSFX.value);
        }
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            sliderMusic.value = PlayerPrefs.GetFloat("musicVolume");
            musicBus.setVolume(sliderSFX.value);
        }
        else
        {
            sliderMusic.value = 0.7f;
            PlayerPrefs.SetFloat("musicVolume", sliderMusic.value);
            musicBus.setVolume(sliderSFX.value);
        }
    }

    void Update()
    {
        UpdateSliders();
        if (player._input.pause){
            Toggle(!canvas.enabled);
            player._input.pause = false;
        }
    }

    void UpdateSliders()
    {

        sfxBus.setVolume(sliderSFX.value);
        PlayerPrefs.SetFloat("sfxVolume", sliderSFX.value);

        musicBus.setVolume(sliderMusic.value);
        PlayerPrefs.SetFloat("musicVolume", sliderMusic.value);

        textSFX.text = "Sound Effects Volume (" + Mathf.RoundToInt(sliderSFX.value * 100) + "%)";
        textMusic.text = "Music Volume (" + Mathf.RoundToInt(sliderMusic.value * 100) + "%)";
        PlayerPrefs.Save();
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
        FMODUnity.RuntimeManager.PlayOneShot("event:/Project/Menu/button");
        canvas.enabled = false;
        Time.timeScale = 1f;
        ExitApplication();
    }

    void OnClickResume()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Project/Menu/close");
        canvas.enabled = false;
        Time.timeScale = 1f;
        player.SwitchInputToPlayer();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnClickSettings()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Project/Menu/button");
        core.SetActive(false);
        settingsCore.SetActive(true);
        instructionsCore.SetActive(false);
    }

    void OnClickInstructions()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Project/Menu/button");
        core.SetActive(false);
        settingsCore.SetActive(false);
        instructionsCore.SetActive(true);
    }

    void OnClickBackSettings()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Project/Menu/button");
        core.SetActive(true);
        settingsCore.SetActive(false);
        instructionsCore.SetActive(false);
    }

    void OnClickBackInstructions()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Project/Menu/button");
        core.SetActive(true);
        settingsCore.SetActive(false);
        instructionsCore.SetActive(false);
    }

    void ExitApplication()
    {
        PlayerPrefs.DeleteKey("WelcomePanel");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
