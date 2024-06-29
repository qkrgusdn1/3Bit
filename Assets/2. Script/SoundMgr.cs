using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
    private static SoundMgr instance;
    public float musicVolume;
    public float attackSoundVolume;
    public AudioSource inGameMusic;
    public List<AudioSource> attackSounds = new List<AudioSource>();
    public AudioSource lobbyMusic;
    public static SoundMgr Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            musicVolume = PlayerPrefs.GetFloat("MusicVolume", musicVolume);
            inGameMusic.volume = musicVolume;
            lobbyMusic.volume = musicVolume;
            attackSoundVolume = PlayerPrefs.GetFloat("AttackSoundVolume", attackSoundVolume);
            foreach (var attackSound in attackSounds)
            {
                attackSound.volume = attackSoundVolume;
            }
        }
        else
        {
            // 인스턴스가 이미 존재하면 현재 인스턴스를 파괴한다.
            Destroy(gameObject);
        }
    }
}
