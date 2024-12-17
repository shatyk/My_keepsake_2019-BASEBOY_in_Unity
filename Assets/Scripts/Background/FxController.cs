using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        if(transform.position.x <= -15)
        {
            transform.position = new Vector3(15,0,4);
        }
    }

    //////////////////////////////////////public////////////////////////////////////

    public void ScaleFx(float newFxScale)
    {
        transform.localScale = new Vector3(newFxScale, newFxScale, transform.localScale.z);
    }
}
