using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    private CharacterAttributes player;
    private AudioMixerGroup mixer;

    //public AudioMixer master;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAttributes>();
        mixer = GetComponent<AudioSource>().outputAudioMixerGroup;
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.outputAudioMixerGroup = mixer;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    
    private void Update()
    {
        //FIXME potentially change it so it is playerHealth / playerMaxHealth so it is more dependent
        if ((float) player.GetHealth() / player.GetMaxHealth() <= 0.25f)
        {
            mixer.audioMixer.SetFloat("Pitch", 0.6f);
        }
        else
        {
            mixer.audioMixer.SetFloat("Pitch", 1f);
        }
    }
    
}
