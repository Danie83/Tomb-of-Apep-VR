using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI audioVolumeText;
    private Slider volumeSlider;

    public float Volume { get; set; } = 1.0f;

    public void Start()
    {
        volumeSlider = GetComponent<Slider>();
        UpdateVolumeText();
    }

    public void UpdateVolumeText()
    {
        if (volumeSlider == null) return;

        Volume = volumeSlider.value;
        if (audioVolumeText != null)
        {
            string volumeText = Mathf.RoundToInt(Volume * 100.0f).ToString() + "%"; 
            audioVolumeText.SetText(volumeText);
        }
    }
}
