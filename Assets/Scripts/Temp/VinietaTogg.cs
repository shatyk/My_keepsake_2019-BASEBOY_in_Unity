using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VinietaTogg : MonoBehaviour
{
    [SerializeField] private GameObject vinieta;
    private Toggle vinTogg;
    // Start is called before the first frame update
    void Start()
    {
        vinTogg = GetComponent<Toggle>();
        vinTogg.Select();
        vinTogg.onValueChanged.AddListener((value) =>
        {
            ToggListener(value);
        });
    }

    public void ToggListener(bool value)
    {
        vinieta.SetActive(value);
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    
}
