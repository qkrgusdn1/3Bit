using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    public bool attackSound;
    public Slider musicSlider;
    public Slider attackSoundSlider;
    public Slider sensitivitySlider;
    float sensitivity;
    private void Start()
    {
        musicSlider.value = SoundMgr.Instance.inGameMusic.volume;
        musicSlider.value = SoundMgr.Instance.lobbyMusic.volume;
        for (int i = 0; i < SoundMgr.Instance.attackSounds.Count; i++)
        {
            attackSoundSlider.value = SoundMgr.Instance.attackSounds[i].volume;
        }

        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", 1.0f);
    }

    public void MusicVolumeSlider(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        SoundMgr.Instance.inGameMusic.volume = musicSlider.value;
        SoundMgr.Instance.lobbyMusic.volume = musicSlider.value;
        SoundMgr.Instance.musicVolume = musicSlider.value;
    }

    public void AttackSoundVolumeSlider(float volume)
    {
        PlayerPrefs.SetFloat("AttackSoundVolume", attackSoundSlider.value);
        for (int i = 0; i < SoundMgr.Instance.attackSounds.Count; i++)
        {
            SoundMgr.Instance.attackSounds[i].volume = attackSoundSlider.value;
        }
        SoundMgr.Instance.attackSoundVolume = attackSoundSlider.value;
    }

    public void SensitivitySlider(float sensitivityValue)
    {
        sensitivity = sensitivityValue;
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
    }
}
