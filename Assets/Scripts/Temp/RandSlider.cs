using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandSlider : MonoBehaviour
{
    [SerializeField] private SpawnFlyingObject spawnLogic;
    [SerializeField] private Text randValText;

    private Slider randSlider;
    // Start is called before the first frame update
    void Start()
    {
        randSlider = GetComponent<Slider>();
        randSlider.Select();
        randSlider.onValueChanged.AddListener((value) =>
        {
            ToggListener(value);
        });

    }

    public void ToggListener(float value)
    {
        spawnLogic.SetBoundsRandCd(value);
        randValText.text = value.ToString();
    }

    public void SetMaxFromCd(float cd)
    {
        float maxVal = (cd / 2f) - 0.20f;
        if (maxVal < 0) maxVal = 0;
        float oldMaxVal = randSlider.maxValue;
        randSlider.maxValue = maxVal;
        if (oldMaxVal == 0 || maxVal == 0)
        {
            randSlider.value = 0;
        } else
        {
            randSlider.value = ((maxVal - randSlider.minValue) * randSlider.value) / (oldMaxVal - randSlider.minValue);       
        }
    }

    public void SetValue(float val)
    {
        randSlider.value = val;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
