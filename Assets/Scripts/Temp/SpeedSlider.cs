using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedSlider : MonoBehaviour
{
    [SerializeField] private SpawnFlyingObject spawnLogic;
    [SerializeField] private Slider cdSlider;
    [SerializeField] private Text speedValText;

    private Slider spdSlider;
    private bool isFirst;

    // Start is called before the first frame update
    void Start()
    {
        spdSlider = GetComponent<Slider>();
        spdSlider.Select();
        spdSlider.onValueChanged.AddListener((value) =>
        {
            ToggListener(value);
        });
        isFirst = true;
    }

    public void ToggListener(float value)
    {
        if (!isFirst) spawnLogic.SetNewFlyObjSpecif(cdSlider.value, value);
        else isFirst = false;
        speedValText.text = value.ToString();       
    }

    public void SetValue(float val)
    {
        spdSlider.value = val;
       
    }

    public void Enabled(bool en)
    {
        spdSlider.interactable = en;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
