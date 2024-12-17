using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyObjChar : MonoBehaviour
{
    private ScreenPart screenPart;
    [SerializeField] private float downBoundSpawn;
    [SerializeField] private float upBoundSpawn;

    public void SetScreenPart(ScreenPart _screenPart)
    {
        screenPart = _screenPart;
    }

    public ScreenPart GetScreenPart()
    {
        return screenPart;
    }

    public float GetDownBoundSpawn()
    {
        return downBoundSpawn;
    }

    public float GetUpBoundSpawn()
    {
        return upBoundSpawn;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
