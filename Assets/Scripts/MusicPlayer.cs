using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;

	public AudioClip StartClip;
	public AudioClip GameClip;
	public AudioClip EndClip;


	private AudioSource Music;
	void Awake()
	{
		
	}
	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			Music = GetComponent<AudioSource> ();
			Music.clip = StartClip;
			Music.loop = true;
			Music.Play ();
		}

	}

	void OnLevelWasLoaded(int level)
	{
		Debug.Log ("MusicPlayer: Loaded Level " + level);

		if(Music)
		{
			Music.Stop ();

			if(level == 0)
			{
				Music.clip = StartClip;
			}else if(level == 1)
			{
				Music.clip = GameClip;
			}
			else if(level == 2)
			{
				Music.clip = EndClip;
			}
			Music.loop = true;
			Music.Play ();
		}

	}
}
