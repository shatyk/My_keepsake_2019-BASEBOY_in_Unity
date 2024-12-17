using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundTogg : MonoBehaviour
{
    private Toggle backgrTogg;
    [SerializeField] private TimeScalerCamera timeScalerCamera;
    // Start is called before the first frame update
    void Start()
    {
        backgrTogg = GetComponent<Toggle>();
        backgrTogg.Select();
        backgrTogg.onValueChanged.AddListener((value) =>
        {
            ToggListener(value);
        });
    }

    public void ToggListener(bool value)
    {
        timeScalerCamera.setScaler(value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
