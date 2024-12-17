using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHandl : MonoBehaviour
{
    private BonusesVariants typeBonus; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ///////////////////////////public/////////////////////
    
    public void SetTypeBonus(BonusesVariants _typeBonus)
    {
        typeBonus = _typeBonus;
    }

    public BonusesVariants GetTypeBonus()
    {
        return typeBonus;
    }
}
