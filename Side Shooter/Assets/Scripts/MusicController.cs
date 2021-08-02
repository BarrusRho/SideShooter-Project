using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;

    public AudioSource levelAudio, bossAudio, victoryAudio, gameOverAudio;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        levelAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopMusic() 
    {
        levelAudio.Stop();
        bossAudio.Stop();
        victoryAudio.Stop();
        gameOverAudio.Stop();
    }

    public void PlayBossAudio() 
    {
        StopMusic();        
        bossAudio.Play();
    }

    public void PlayVictoryAudio() 
    {
        StopMusic();
        victoryAudio.Play();
    }

    public void PlayGameOverAudio() 
    {
        StopMusic();
        gameOverAudio.Play();
    }
}
