using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] sfx;
    public AudioSource[] bgm;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(int soundToPlay)
    {
        if (soundToPlay < sfx.Length)
            sfx[soundToPlay].Play();
        else
            Debug.Log("Sound Effect " + soundToPlay + " don't exist");
    }
    public void PlayBGM(int musicToPlay)
    {
        StopMusic();
        if (musicToPlay < bgm.Length)
            bgm[musicToPlay].Play();
        else
            Debug.Log("Music " + musicToPlay + " don't exist");
    }
    public void StopMusic()
    {
        foreach (var music in bgm)
            music.Stop();
    }
}
