using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactTriggered : MonoBehaviour
{
    private ScreenPart screenPart;

    // Start is called before the first frame update
    void Start()
    {
        screenPart = GetComponent<FlyObjChar>().GetScreenPart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ///////////////////////////////////public//////////////////////////

    public void KickHit()
    {
        Destroy(this.gameObject);
    }
}
