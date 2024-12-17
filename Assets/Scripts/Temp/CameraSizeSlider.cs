using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSizeSlider : MonoBehaviour
{
    [SerializeField] private TimeScalerCamera timeScalerCamera;
    [SerializeField] private SpawnFlyingObject spawnLogic;
    [SerializeField] private Text textOrtSize;

    private Slider sizeSlider;
    // Start is called before the first frame update
    void Start()
    {
        sizeSlider = GetComponent<Slider>();
        sizeSlider.Select();
        sizeSlider.onValueChanged.AddListener((value) =>
        {
            ToggListener(value);
        });
    }

    public void ToggListener(float value)
    {
        timeScalerCamera.SetOrtSize(value);
        spawnLogic.CalcFlyObjSpecif();
        textOrtSize.text = value.ToString();
    }

    public void SetSliderValue(float value)
    {
        sizeSlider.value = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
