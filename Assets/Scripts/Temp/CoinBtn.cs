using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinBtn : MonoBehaviour
{
    [SerializeField] private BonusManager bonusManager;

    private Button btnCoin;
    private BonusesVariants botVar;

    // Start is called before the first frame update
    void Start()
    {
        btnCoin = GetComponent<Button>();
        btnCoin.Select();
        btnCoin.onClick.AddListener(() =>
        {
            BtnListener();
        });
        botVar = BonusesVariants.COIN;
    }

    public void BtnListener()
    {
        bonusManager.StartBonus(botVar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
