using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgrController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= -17)
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }
        
    }

    //////////////////////////////////////public////////////////////////////////////

    public void ScaleBackgr(float newBackgrScale)
    {
        transform.localScale = new Vector3(newBackgrScale, newBackgrScale, transform.localScale.z);
    }
}
