using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager uIManager;
    private void Start()
    {
        CoinCalculator(0);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&&gameObject.CompareTag("FinishLine"))
        {
            Debug.Log("game over");
            CoinCalculator(100);
            uIManager.CoinTextUpdate();
            uIManager.FinishScreen();
        }
    }

    public void CoinCalculator(int money)
    {
        if (PlayerPrefs.HasKey("moneyy"))
        {
            int oldScore = PlayerPrefs.GetInt("moneyy");

            PlayerPrefs.SetInt("moneyy", oldScore+money);
        }
        else
        {
            PlayerPrefs.SetInt("moneyy", 0);
        }
    }



}
