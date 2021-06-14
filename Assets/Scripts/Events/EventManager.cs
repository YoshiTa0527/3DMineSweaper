using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager
{
    public static event Action OnGameOver;

    /// <summary>
    ///ゲームオーバー時に呼ぶ
    /// </summary>
    public static void GameOver()
    {
        OnGameOver?.Invoke();
    }
}
