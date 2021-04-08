using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    public float Player_life = 1000;
    public float Player_speed = 17;
    public float Player_jump_force = 25;
    public bool Player_doublejump = true;
    public int Coins_Gathered;
    public int Gems_Gathered;
    public float Player_Arrow_Damage=50;
    public float Player_Arrow_LVL2_Damage=200;


    public void AddGold(int x)
    {
        Coins_Gathered += x;
    }
}

