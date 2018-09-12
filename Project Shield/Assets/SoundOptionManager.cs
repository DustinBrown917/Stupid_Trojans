﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOptionManager : MonoBehaviour {

    private AudioSource audioSource;
    private bool playSound = true;
    private WaitForSeconds wfs = new WaitForSeconds(0.07f);

    [SerializeField]
    private Slider masterSlider;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        masterSlider.value = MusicManager.Instance.GetMasterVolFactor();
        musicSlider.value = MusicManager.Instance.GetMusicVolFactor();
        sfxSlider.value = MusicManager.Instance.GetSfxVolFactor();
    }

    public void PlayTestAudio()
    {
        if (!playSound) { return; }
        audioSource.Play();
        playSound = false;
        StartCoroutine(ResetPlaySound());
    }

    private IEnumerator ResetPlaySound()
    {
        yield return wfs;
        playSound = true;
    }
}
