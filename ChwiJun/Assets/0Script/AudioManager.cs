using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    AudioSource bgm;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //bgm = gameObject.GetComponent<AudioSource>();
        //bgm.Play();
        DontDestroyOnLoad(gameObject);
    }
    
    public void BgmPlay()
    {
        if(Instance == null)
        {
            bgm = gameObject.GetComponent<AudioSource>();
        }
        else
        {
            bgm = Instance.GetComponent<AudioSource>();
            bgm.Play();
        }
    }

    public void BgmPause()
    {
        if (Instance == null)
        {
            bgm = gameObject.GetComponent<AudioSource>();
        }
        else
        {
            bgm = Instance.GetComponent<AudioSource>();
            bgm.Pause();
        }
    }

    public void BgmStop()
    {
        if (Instance == null)
        {
            bgm = gameObject.GetComponent<AudioSource>();
        }
        else
        {
            bgm = Instance.GetComponent<AudioSource>();
            bgm.Stop();
        }
    }   
}
