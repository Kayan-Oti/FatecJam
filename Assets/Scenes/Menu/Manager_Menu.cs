using UnityEngine;
using UnityEngine.EventSystems;

public class Manager_Menu : MonoBehaviour
{
    [Header("Play")]
    [SerializeField] private GameObject _playButton;
    [Header("Settings")]
    [SerializeField] private GameObject _settingsContainer;
    [SerializeField] private GameObject _settingsButton;
    [SerializeField] private GameObject _settingsFirstButton;
    [Header("Credits")]
    [SerializeField] private GameObject _creditsContainer;
    [SerializeField] private GameObject _creditsButton;
    [SerializeField] private GameObject _creditsFirstButton;

    [Header("Levels")]
    [SerializeField] private SceneIndex _playScene;


    private void OnEnable() {
        Manager_Event.GameManager.OnLoadedScene.Get().AddListener(OnLoadScene);
    }

    private void OnDisable() {
        Manager_Event.GameManager.OnLoadedScene.Get().RemoveListener(OnLoadScene);
    }

    private void Start(){
        Invoke(nameof(WaitStart),0.1f);
    }
    private void WaitStart(){
        EventSystem.current.SetSelectedGameObject(_playButton);
        _settingsContainer.SetActive(false);
        _creditsContainer.SetActive(false);
    }

    #region Scene Management
    private void OnLoadScene() {
        //Play Song Menu
        AudioManager.Instance.InitializeMusic(FMODEvents.Instance.MenuMusic, MusicIntensity.Intensity3);
    }

    private void LeavingMenu(){
        //Stop Song
        AudioManager.Instance.StopMusic();
    }

    #endregion

    #region Onclick

    public void OnClick_Play(){
        LeavingMenu();
        GameManager.Instance.LoadScene(_playScene);
    }

    //--Menu Settings
    public void OnClick_Settings(){
        Debug.Log("OnClick_Settings");
        _settingsContainer.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_settingsFirstButton);
    }

    public void OnClick_SettingsClose(){
        Debug.Log("OnClick_SettingsClose");
        _settingsContainer.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_settingsButton);
    }

    //--Menu Credits
    public void OnClick_Credits(){
        Debug.Log("OnClick_Credits");
        _creditsContainer.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_creditsFirstButton);
    }

    public void OnClick_CreditsClose(){
        Debug.Log("OnClick_CreditsClose");
        _creditsContainer.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_creditsButton);
    }

    //--Menu Exit
    public void OnClick_Exit(){
        Application.Quit();
    }

    #endregion
}
