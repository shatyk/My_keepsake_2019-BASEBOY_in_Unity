using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBtn : MonoBehaviour
{
    [SerializeField] private BonusManager bonusManager;

    private Button btnShield;
    private BonusesVariants botVar;

    // Start is called before the first frame update
    void Start()
    {
        btnShield = GetComponent<Button>();
        btnShield.Select();
        btnShield.onClick.AddListener(() =>
        {
            BtnListener();
        });
        botVar = BonusesVariants.SHIELD;
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
