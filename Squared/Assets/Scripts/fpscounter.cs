using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class fpscounter : MonoBehaviour
{
    public TextMeshProUGUI fpscount;
    private float fps;
    private void Awake()
    {
        Application.targetFrameRate = 120;
    }
    private void Update()
    {
        fps = Mathf.Round(1000 / (Time.deltaTime * 1000));
        // Debug.Log(fps);
        fpscount.text = fps.ToString();
    }

}
