using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScalerCamera : MonoBehaviour
{
    [SerializeField] BackgrController fonContr;
    [SerializeField] FxController fxContr;
    [SerializeField] VinietaController vinContr;

    [SerializeField] private CameraSizeSlider cameraSlider;

    [SerializeField] private float scaleSpeed; 
    [SerializeField] private float scaleSpeedBackgr;
    [SerializeField] private float fullBackgrOrtSize; //ортсайз для камеры, при котором: высота обзора камеры == высота фона

    private Camera cameraComp;

    private float backgrScaleCoef;
    private bool isScaler = true;
    private float startOrtSize;

    private bool isScale = true; //temp

    // Start is called before the first frame update
    void Start()
    {
        cameraComp = GetComponent<Camera>();
        startOrtSize = backgrScaleCoef = cameraComp.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(isScale)
        {
            SetOrtSize(30);
            isScale = false;
 */
            cameraComp.orthographicSize += scaleSpeed * Time.deltaTime;
            if (backgrScaleCoef <= 21) backgrScaleCoef += scaleSpeedBackgr * Time.deltaTime;
            float fonScale;
            if (isScaler)
            {
                fonScale = cameraComp.orthographicSize / backgrScaleCoef;
            } else
            {
                fonScale = cameraComp.orthographicSize / fullBackgrOrtSize;
            }          
            fonContr.ScaleBackgr(fonScale);
            fxContr.ScaleFx(cameraComp.orthographicSize / startOrtSize);
            vinContr.ScaleVinieta(cameraComp.orthographicSize / startOrtSize);

            cameraSlider.SetSliderValue(cameraComp.orthographicSize);
        //}
    }

    ///////////////////////////////////////////public////////////////////////////////

    public void setScaler(bool val)
    {
        isScaler = val;
    }

    public float retScaleSpeed()
    {
        return scaleSpeed;
    }

    public void SetOrtSize(float val)
    {
        cameraComp.orthographicSize = val;
    }

}
