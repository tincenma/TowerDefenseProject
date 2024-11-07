using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour {

	public static int Money;
	public int startMoney = 400;

	public static int Lives;
	public int startLives = 20;

	public static int Rounds;

    public static Action<int> OnLivesChanged;

    public static void ResetStats()
    {
        Money = 400;
        Lives = 20;
        Rounds = 0;

        OnLivesChanged?.Invoke(Lives);
    }

    public static void TakeDamage(int damage)
    {
        Lives -= damage;
        OnLivesChanged?.Invoke(Lives);
    }

    void Start ()
	{
        ResetStats();
    }
}
