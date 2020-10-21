using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSounds : MonoBehaviour {

	public AudioClip[] HurtSounds;
	public AudioClip WingFlap;

	AudioSource source;

	// Use this for initialization
	void Start ()
	{
		source = GetComponent<AudioSource>();
	}
	
	public void PlayHurtSound()
	{
		int index = Random.Range(0, HurtSounds.Length);
		AudioClip clip = HurtSounds[index];

		source.pitch = Random.Range(0.9f, 1.1f);
		source.volume = 0.5f;

		source.PlayOneShot(clip);
	}

	public void PlayWingFlap()
	{
		source.volume = 0.7f;
		source.pitch = Random.Range(0.95f, 1.05f);

		source.PlayOneShot(WingFlap);
	}

}
