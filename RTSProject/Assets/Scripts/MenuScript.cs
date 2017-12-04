using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public GameObject playButton;
    public GameObject settingsButton;
    public GameObject mapEditorButton;
    public GameObject quitButton;

    void Start()
    {
        playButton = GameObject.FindGameObjectWithTag("playButton");
        settingsButton = GameObject.FindGameObjectWithTag("settingsButton");
        mapEditorButton = GameObject.FindGameObjectWithTag("mapEditorButton");
        quitButton = GameObject.FindGameObjectWithTag("quitButton");
    }

    public void PlayButtonClicked()
    {
        SceneManager.LoadScene("Prototyping Scene");
    }

    public void SettingsButtonClicked()
    {
        //TODO Implement settings: Audio, Graphics, Miscelaneous, Hotkeys
    }

    public void MapEditorButtonClicked()
    {
        SceneManager.LoadScene("Map Editor Scene");
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
