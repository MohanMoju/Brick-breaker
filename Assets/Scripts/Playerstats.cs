using UnityEngine;

[CreateAssetMenu(fileName = "stats", menuName = "PlayerStats", order = 4)]
public class Playerstats : ScriptableObject
{
   public int level = 1;
    public int lives = 3;
    public int score = 0;
}
