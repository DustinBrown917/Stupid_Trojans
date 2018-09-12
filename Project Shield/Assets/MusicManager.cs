using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour {

    public static MusicManager Instance { get; private set; }

    [SerializeField]
    private AudioClip menuMusic;
    [SerializeField]
    private AudioClip gameMusic;

    [SerializeField]
    private float maxVol = 0;
    [SerializeField]
    private float minVol = -65;

    private AudioSource audioSource;

    [SerializeField]
    private AudioMixer masterMixer;

    private string masterVolString = "masterVol";
    private string musicVolString = "musicVol";
    private string sfxVolString = "sfxVol";

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        
    }

    // Use this for initialization
    void Start () {
        GameManager.Instance.GameStateChanged += GameManager_GameStateChanged;
        LoadSoundVol();
    }

    private void GameManager_GameStateChanged(object sender, GameManager.GameStateChangedArgs e)
    {
        if(e.newState == GameManager.GameState.PLAYING)
        {
            audioSource.clip = gameMusic;
        } else
        {
            audioSource.clip = menuMusic;
        }

        audioSource.Play();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void SetMasterVol(float masterVol)
    {
        masterMixer.SetFloat(masterVolString, Mathf.Lerp(minVol, maxVol, masterVol));
    }

    public void SetMusicVol(float musicVol)
    {
        masterMixer.SetFloat(musicVolString, Mathf.Lerp(minVol, maxVol, musicVol));
    }

    public void SetSFXVol(float sfxVol)
    {
        masterMixer.SetFloat(sfxVolString, Mathf.Lerp(minVol, maxVol, sfxVol));
    }

    private void SaveSoundVol()
    {
        float master;
        float music;
        float sfx;

        masterMixer.GetFloat(masterVolString, out master);
        masterMixer.GetFloat(musicVolString, out music);
        masterMixer.GetFloat(sfxVolString, out sfx);

        PlayerPrefs.SetFloat(masterVolString, master);
        PlayerPrefs.SetFloat(musicVolString, music);
        PlayerPrefs.SetFloat(sfxVolString, sfx);
    }

    private void LoadSoundVol()
    {
        
        if (PlayerPrefs.HasKey(masterVolString))
        {          
            masterMixer.SetFloat(masterVolString, PlayerPrefs.GetFloat(masterVolString));
        }

        if (PlayerPrefs.HasKey(musicVolString))
        {
            masterMixer.SetFloat(musicVolString, PlayerPrefs.GetFloat(musicVolString));
        }

        if (PlayerPrefs.HasKey(sfxVolString))
        {
            masterMixer.SetFloat(sfxVolString, PlayerPrefs.GetFloat(sfxVolString));
        }
    }

    public float GetMasterVolFloat()
    {
        float vol;
        masterMixer.GetFloat(masterVolString, out vol);

        return vol;
    }

    public float GetMasterVolFactor()
    {
        float vol;
        masterMixer.GetFloat(masterVolString, out vol);

        float factor = (vol - minVol) / (maxVol - minVol);
        return factor;
    }

    public float GetMusicVolFloat()
    {
        float vol;
        masterMixer.GetFloat(musicVolString, out vol);

        return vol;
    }

    public float GetMusicVolFactor()
    {
        float vol;
        masterMixer.GetFloat(musicVolString, out vol);

        float factor = (vol - minVol) / (maxVol - minVol);
        return factor;
    }

    public float GetSfxVolFloat()
    {
        float vol;
        masterMixer.GetFloat(sfxVolString, out vol);

        return vol;
    }

    public float GetSfxVolFactor()
    {
        float vol;
        masterMixer.GetFloat(sfxVolString, out vol);

        float factor = (vol - minVol) / (maxVol - minVol);
        return factor;
    }


    private void OnApplicationQuit()
    {
        SaveSoundVol();
    }
}
