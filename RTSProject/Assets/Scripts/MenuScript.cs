using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    private Button playButton;
    private Button settingsButton;
    private Button mapEditorButton;
    private Button quitButton;

    void Start()
    {
        #region Get Buttons
        playButton = GameObject.FindGameObjectWithTag("playButton").GetComponent<Button>();
        settingsButton = GameObject.FindGameObjectWithTag("settingsButton").GetComponent<Button>();
        mapEditorButton = GameObject.FindGameObjectWithTag("mapEditorButton").GetComponent<Button>();
        quitButton = GameObject.FindGameObjectWithTag("quitButton").GetComponent<Button>();
        #endregion
        #region Add onClick Listeners
        playButton.onClick.AddListener(PlayButtonClicked);
        settingsButton.onClick.AddListener(SettingsButtonClicked);
        mapEditorButton.onClick.AddListener(MapEditorButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);
        #endregion 
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
