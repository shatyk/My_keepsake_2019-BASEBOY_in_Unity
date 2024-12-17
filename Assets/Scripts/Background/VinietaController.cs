using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinietaController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //////////////////////////////////////public////////////////////////////////////

    public void ScaleVinieta(float newVinScale)
    {
        transform.localScale = new Vector3(newVinScale, newVinScale, transform.localScale.z);
    }
}
