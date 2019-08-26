using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioSource audioSourceHeluo;
    public AudioSource audioSourceGege;
    public AudioSource audioSourceBo;
    public AudioSource audioSouceWenyao;
    public AudioSource audioSouceXixi;
    public AudioSource audioSourceRupi;

    private AudioSource currentAudio;
    //Manager
    private BannerManager bannerManager;
    bool lastInBanner;
    

    private void Awake()
    {
        bannerManager = BannerManager.Instance;
        bannerManager.setInBanner(false);

    }


    // Start is called before the first frame update
    void Start()
    {
        lastInBanner = bannerManager.InBanner;
    }

    // Update is called once per frame
    void Update()
    {
        string currentBanner = bannerManager.CurrentBanner;
        if (bannerManager.InBanner == true && lastInBanner == false)
        {
            switch (currentBanner)
            {
                case "gege":
                    currentAudio = audioSourceGege;
                    break;
                case "heluo":
                    currentAudio = audioSourceHeluo;
                    break;
                case "wenyao":
                    currentAudio = audioSouceWenyao;
                    break;
                case "bo":
                    currentAudio = audioSourceBo;
                    break;
                case "xixi":
                    currentAudio = audioSouceXixi;
                    break;
                case "rupi":
                    currentAudio = audioSourceRupi;
                    break;
                default:
                    currentAudio = audioSourceHeluo;
                    break;
            }
            OnEnterBanner(currentAudio);
        }
        else if (bannerManager.InBanner == false && lastInBanner == true)
        {
            OnExitBanner(currentAudio);
        }
        lastInBanner = bannerManager.InBanner;
    }

    private void OnEnterBanner(AudioSource audioSource)
    {
        StartCoroutine(AudioController.FadeIn(audioSource, 1f));

    }

    private void OnExitBanner(AudioSource audioSource)
    {
        StartCoroutine(AudioController.FadeOut(audioSource, 1f));
    }
}
