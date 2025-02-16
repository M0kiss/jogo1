using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] SettingsPanel settingsPanel;
    //Scene sceneName;
    //readonly string tutorial = "Tutorial";
    void Awake()
    {
        //sceneName = SceneManager.GetActiveScene();
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 0;
    }
    private void Start()
    {
        settingsPanel.slider[0].GetComponent<SetVolume>().SetLevel(PlayerPrefs.GetFloat("MasterVol", 1));
        settingsPanel.slider[1].GetComponent<SetVolume>().SetLevel(PlayerPrefs.GetFloat("MusicVol", 1));
        settingsPanel.slider[2].GetComponent<SetVolume>().SetLevel(PlayerPrefs.GetFloat("SFXVol", 1));
    }
}