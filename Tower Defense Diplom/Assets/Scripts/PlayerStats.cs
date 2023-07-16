using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 500;

    public static int Lives;
    public int startLives = 20;

    public static int rounds;
    public static int bestResult;

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;

        rounds = 0;

        if (PlayerPrefs.GetInt("bestResult") != 0)
            bestResult = PlayerPrefs.GetInt("bestResult");
    }

    
}
