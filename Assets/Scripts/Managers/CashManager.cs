using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashManager : MonoBehaviour
{
    private int cash;
    [SerializeField] private Text cashText;

    // Start is called before the first frame update
    void Start()
    {
        cash = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ///////////////////////////////public////////////////////////
    
    public void IncCash(int count)
    {
        cash += count;
        if (cash < 0) cash = 0;
        cashText.text = cash.ToString();
    }
}
