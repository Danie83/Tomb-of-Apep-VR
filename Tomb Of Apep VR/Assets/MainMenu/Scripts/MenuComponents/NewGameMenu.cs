using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewGameMenu : MenuBaseComponent
{
    [SerializeField]
    private TMP_Dropdown difficultySetting;

    private TMP_Dropdown.OptionData[] options;
    private Toggle playTutorial;

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
}
