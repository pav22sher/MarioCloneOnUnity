using UnityEngine;

public class gameInfo{
	public static int life = 3;
	public static int level_time = 400;
	public static Vector2 level = new Vector2(1,1);
	public static int coins_count = 0;
	public static int mario_score=0;
	public static PlayerStatus status=PlayerStatus.Small;
	public static Vector3 position=new Vector3(-5,0,0);
	public static int game_start_time=(int)Time.time;
	public static bool is_level_start=true;

	public static void toStartLevel1_1()
	{
		life = 3;
		level_time = 400;
		level = new Vector2 (1, 1);
		coins_count = 0;
		mario_score = 0;
		status = PlayerStatus.Small;
		position = new Vector3 (-5, 0, 0);
		game_start_time = (int)Time.time;
		is_level_start = true;
	}
}
