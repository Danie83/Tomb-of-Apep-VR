using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NewGameMenu : MenuBaseComponent
{
    [SerializeField]
    private TMP_Dropdown difficultySetting;

    private TMP_Dropdown.OptionData[] options;

    [SerializeField]
    private Toggle playTutorial;

    [SerializeField]
    public int tutorialScene;
    [SerializeField]
    public int mainScene;

    public void Start()
    {
        options = difficultySetting.options.ToArray();
    }

    public string GetDifficultySetting()
    {
        int diffIndex = Mathf.Clamp(difficultySetting.value, 0, options.Length);
        string resultDiff = options[diffIndex].text.ToLower();
        return resultDiff;
    }

    public bool ToPlayTutorial()
    {
        return playTutorial.isOn;
    }

    public void OpenScene(int scene = -1)
    {
        if (scene != -1)
        {
            SceneManager.LoadScene(scene);
            return;
        }
        if (ToPlayTutorial())
            SceneManager.LoadScene(tutorialScene);
        else
            SceneManager.LoadScene(mainScene);


    }
}
