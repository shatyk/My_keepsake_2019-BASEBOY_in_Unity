using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowDownBtn : MonoBehaviour
{
    [SerializeField] private BonusManager bonusManager;

    private Button btnSlowDown;
    private BonusesVariants botVar;

    // Start is called before the first frame update
    void Start()
    {
        btnSlowDown = GetComponent<Button>();
        btnSlowDown.Select();
        btnSlowDown.onClick.AddListener(() =>
        {
            BtnListener();
        });
        botVar = BonusesVariants.SLOWDOWN;
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
