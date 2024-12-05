using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsAdd : MonoBehaviour
{
    public TMP_Text coins;
    public static CoinsAdd instance { get; private set; }
    public PlayerController player;

    public void addCoins()
    {
        player.playercoins += 1;
    }


    // Update is called once per frame
    void Update()
    {
        coins.text = player.playercoins.ToString();
    }
}
