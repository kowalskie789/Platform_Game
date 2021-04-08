using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Display : MonoBehaviour
{

    public Text coinAmount;
    public GameObject Player; // player holds Player_stats script that vontains all important stats, might change later to special empty object on scene for now it works
   
    // Update is called once per frame
    void Update()
    {
        // work in progres kurwa
        coinAmount.text = Player.GetComponent<Player_Stats>().Coins_Gathered.ToString();
    }
}
