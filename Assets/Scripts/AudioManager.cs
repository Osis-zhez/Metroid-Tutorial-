using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioSource mainMenuMusic, levelMusic, bossMusic;
    [SerializeField] AudioSource[] sfx;
    [SerializeField] private AudioMixerGroup audioMixer;

    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }


    public void PlayMainMenuMusic()
    {
        levelMusic.Stop();
        bossMusic.Stop();
        mainMenuMusic.Play();
    }

    public void PlayLevelMusic()
    {
        if(!levelMusic.isPlaying)
        {
            levelMusic.Play();
            bossMusic.Stop();
            mainMenuMusic.Stop();
        }
    }

    public void PlayBossMusic()
    {
        levelMusic.Stop();
        bossMusic.Play();
    }

    public void PlaySFX(int indexSFX)
    {
        sfx[indexSFX].Stop();
        sfx[indexSFX].Play();
        mainMenuMusic.outputAudioMixerGroup = audioMixer;
    }

    public void PlaySFXAdjucted(int indexSFXAdjusted)
    {
        sfx[indexSFXAdjusted].pitch = Random.Range(0.8f, 1.2f);
        PlaySFX(indexSFXAdjusted);
    }


}
