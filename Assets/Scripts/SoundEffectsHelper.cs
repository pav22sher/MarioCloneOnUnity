using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsHelper : MonoBehaviour
{
	public static SoundEffectsHelper Instance;

	public AudioClip bowser_falls;
	public AudioClip bowser_fire;

	public AudioClip brick_big;
	public AudioClip brick_small;

	public AudioClip coins;
	public AudioClip down_goomba_koopa;

	public AudioClip fireball_bang;
	public AudioClip fireball_kill;
	public AudioClip fireball_shot;

	public AudioClip jump_big;
	public AudioClip jump_small;

	public AudioClip koopa_bump;
	public AudioClip koopa_touch;

	public AudioClip life_plus;

	public AudioClip show_bonus;
	public AudioClip transformation;
	public AudioClip pipes;

	void Awake()
	{
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of SoundEffectsHelper!");
		}
		Instance = this;
	}

	public void Make_bowser_falls_Sound()
	{
		MakeSound(bowser_falls);
	}
	public void Make_bowser_fire_Sound()
	{
		MakeSound(bowser_fire);
	}
	public void Make_brick_big_Sound()
	{
		MakeSound(brick_big);
	}
	public void Make_brick_small_Sound()
	{
		MakeSound(brick_small);
	}
	public void Make_coins_Sound()
	{
		MakeSound(coins);
	}
	public void Make_down_goomba_koopa_Sound()
	{
		MakeSound(down_goomba_koopa);
	}
	public void Make_fireball_bang_Sound()
	{
		MakeSound(fireball_bang);
	}
	public void Make_fireball_kill_Sound()
	{
		MakeSound(fireball_kill);
	}
	public void Make_fireball_shot_Sound()
	{
		MakeSound(fireball_shot);
	}
	public void Make_jump_big_Sound()
	{
		MakeSound(jump_big);
	}
	public void Make_jump_small_Sound()
	{
		MakeSound(jump_small);
	}
	public void Make_koopa_bump_Sound()
	{
		MakeSound(koopa_bump);
	}
	public void Make_koopa_touch_Sound()
	{
		MakeSound(koopa_touch);
	}
	public void Make_life_plus_Sound()
	{
		MakeSound(life_plus);
	}
	public void Make_show_bonus_Sound()
	{
		MakeSound(show_bonus);
	}
	public void Make_transformation_Sound()
	{
		MakeSound(transformation);
	}
	public void Make_pipes_Sound()
	{
		MakeSound(pipes);
	}

	private void MakeSound(AudioClip originalClip)
	{
		AudioSource.PlayClipAtPoint(originalClip, transform.position);
	}
}
