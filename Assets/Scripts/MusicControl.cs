using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicControl : MonoBehaviour
{
    public static MusicControl Instance;
    public AudioMixer audioMixer;
    public AudioSource audioSource;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }  
        DontDestroyOnLoad(Instance);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
