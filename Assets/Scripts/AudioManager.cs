using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public Sound[] sounds;

    void Awake() {
        foreach (Sound s in sounds) {
        	s.source = gameObject.AddComponent<AudioSource>();
        	s.source.clip = s.clip;
        }
    }

    public void Play (string name) {
    	Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning("Sound: " + name + "not found!");
			return;
		}
		s.source.Play();
		if (PauseMenu.GameIsPaused) {
			s.source.Pause();
			s.source.enabled = false;
		}
	}

	public void Stop (string name) {
    	Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning("Sound: " + name + "not found!");
			return;
		}
		s.source.Stop();
	}

}