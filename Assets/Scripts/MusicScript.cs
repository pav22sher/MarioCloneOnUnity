using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
	public AudioClip level_1;
	public AudioClip level_2;
	public AudioClip level_4;
	public AudioClip star;
	public AudioClip flag;
	public AudioClip end;
	public AudioClip game_over;
	public AudioClip time_over;
	public AudioClip win;
	public AudioClip minus_life;
	public AudioClip in_pipe;

	private AudioSource music;

	void Awake()
	{
		music = GetComponent<AudioSource> ();
	}

	public void Make_level_1_Music(){
		music.clip = level_1;
		music.Play ();
		music.loop = true;
	}
	public void Make_level_2_Music(){
		music.clip = level_2;
		music.Play ();
		music.loop = true;
	}
	public void Make_level_4_Music(){
		music.clip = level_4;
		music.Play ();
		music.loop = true;
	}

	public void Make_star_Music(){
		music.Pause ();
		music.clip = star;
		music.Play ();
	}
	public void Make_flag_Music(){
		music.Pause ();
		music.loop = false;
		music.clip = flag;
		music.Play ();
	}
	public void Make_end_Music(){
		music.Pause ();
		music.loop = false;
		music.clip = end;
		music.Play ();
	}
	public void Make_game_over_Music(){
		music.Pause ();
		music.loop = false;
		music.clip = game_over;
		music.Play ();
	}
	public void Make_time_over_Music(){
		music.Pause ();
		music.loop = false;
		music.clip = time_over;
		music.Play ();
	}
	public void Make_win_Music(){
		music.Pause ();
		music.loop = false;
		music.clip = win;
		music.Play ();
	}
	public void Make_minus_life_Music(){
		music.Pause ();
		music.loop = false;
		music.clip = minus_life;
		music.Play ();
	}
	public void Make_in_pipe_Music(){
		music.Pause ();
		music.loop = true;
		music.clip = in_pipe;
		music.Play ();
	}
}
