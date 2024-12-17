using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartBtn : MonoBehaviour
{
    private Button btnRestart;
    // Start is called before the first frame update
    void Start()
    {
        btnRestart = GetComponent<Button>();
        btnRestart.Select();
        btnRestart.onClick.AddListener(() =>
        {
            ToggListener();
        });
    }

    public void ToggListener()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
