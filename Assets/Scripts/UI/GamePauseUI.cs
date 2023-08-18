using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button mainMenuButton;
    

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            KitchenGameManager.Instance.TogglePauseGame();
        });

        optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.gameObject.SetActive(true);
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;

        //Hide();    // idk why i have to comment this in order to work normally...
        // the pauseUI is activated in the gamescene hierarchy and should display at the beginning
        // which is why it normally should be hidden in the Start()
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void KitchenGameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
