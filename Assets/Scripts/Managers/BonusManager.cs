using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private Text textCoin;
    [SerializeField] private Text textSlowDown;
    [SerializeField] private Text textShield;
    [SerializeField] private CollisionManager collisManager;
    [SerializeField] private SpawnFlyingObject spwnFlyObjManager;

    [SerializeField] private int neededNum;
    //shield
    [SerializeField] private float durShieldBonusSec; //длительность щита
    [SerializeField] private int howManyItemsShieldProtect; //сколько предметов щит отбивает
    //slowdown
    [SerializeField] private float durSlowDownBonusSec; //длительность замедления 
    [SerializeField] private float percentSlowDown; // на сколько процентов замедление
    //cash
    [SerializeField] private float durCoinBonus; //длительность кеш бонуса
    [SerializeField] private int howManyCoinsInc; //сколько монеток прибавляется

    private int[] progrArr = { 0, 0, 0 };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ////////////////////////////public/////////////////////////

    public void IncProgr(BonusesVariants bonVar)
    {
        if (progrArr[(int)bonVar] < neededNum) progrArr[(int)bonVar]++;
        switch(bonVar)
        {
            case BonusesVariants.COIN:
                textCoin.text = progrArr[(int)bonVar].ToString();
                break;

            case BonusesVariants.SHIELD:
                textShield.text = progrArr[(int)bonVar].ToString();
                break;

            case BonusesVariants.SLOWDOWN:
                textSlowDown.text = progrArr[(int)bonVar].ToString();
                break;
        }
    }

    public void ResetProgr(BonusesVariants bonVar)
    {
        progrArr[(int)bonVar] = 0;
        switch (bonVar)
        {
            case BonusesVariants.COIN:
                textCoin.text = "0";
                break;

            case BonusesVariants.SHIELD:
                textShield.text = "0";
                break;

            case BonusesVariants.SLOWDOWN:
                textSlowDown.text = "0";
                break;
        }
    }

    public void StartBonus(BonusesVariants bonVar)
    {      
        if(progrArr[(int)bonVar] < neededNum)
        {
            Debug.Log(bonVar + " < " + neededNum);
        } else
        {
            switch (bonVar)
            {
                case BonusesVariants.COIN:
                    collisManager.StartCoinBonus(durCoinBonus, howManyCoinsInc);
                    break;

                case BonusesVariants.SHIELD:
                    collisManager.StartShieldBonus(durShieldBonusSec, howManyItemsShieldProtect);
                    break;

                case BonusesVariants.SLOWDOWN:
                    spwnFlyObjManager.StartSlowDownBonus(durSlowDownBonusSec, percentSlowDown);
                    break;
            }
            ResetProgr(bonVar);
        }

        
    }
}
