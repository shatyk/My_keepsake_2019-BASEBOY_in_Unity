using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CdSlider : MonoBehaviour
{
    [SerializeField] private SpawnFlyingObject spawnLogic;
    [SerializeField] private Slider spdSlider;
    [SerializeField] private RandSlider randSlider;
    [SerializeField] private Text cdValText;

    private Slider cdSlider;
   

    // Start is called before the first frame update
    void Start()
    {
        cdSlider = GetComponent<Slider>();
        cdSlider.Select();
        cdSlider.onValueChanged.AddListener((value) =>
        {
            ToggListener(value);
        });
    }

    public void ToggListener(float value)
    {
        randSlider.SetMaxFromCd(value);
        spawnLogic.SetNewFlyObjSpecif(value, spdSlider.value);
        cdValText.text = value.ToString();
    }

    public void SetValue(float val)
    {
        randSlider.SetMaxFromCd(val);
        cdSlider.value = val;
    }

    public void Enabled(bool en)
    {
        cdSlider.interactable = en;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
