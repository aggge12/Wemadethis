using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public List<AudioClip> audioClips;
    private AudioSource audioSource; 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(string name){
        if (audioClips?.Count > 0){
            var clip = audioClips.FirstOrDefault(x => x.name == name);
            if (clip){
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }
}
