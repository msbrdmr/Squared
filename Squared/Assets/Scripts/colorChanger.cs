using UnityEngine;
using System;
using System.Collections;
using TMPro;
 
 
public class colorChanger : MonoBehaviour
{
    public Color StartColor;
    public Color EndColor;
    public float time;
    bool goingForward;
    bool isCycling;

    public TextMeshProUGUI scoreText;
    Material myMaterial;
 
    private void Awake()
    {
        goingForward = true;
        isCycling = false;
        myMaterial = GetComponent<Renderer>().material;
    }
 
    private void Update()
    {
        if (!isCycling)
        {
            if (goingForward)
                StartCoroutine(CycleMaterial(StartColor, EndColor, time,myMaterial));
            else
                StartCoroutine(CycleMaterial(EndColor, StartColor, time, myMaterial));
        }
    }
 
    IEnumerator CycleMaterial(Color startColor, Color endColor, float cycleTime, Material mat)
    {
        isCycling = true;
        float currentTime = 0;
        while (currentTime < cycleTime)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / cycleTime;
            Color currentColor = Color.Lerp(startColor, endColor, t);
            mat.color = currentColor;
            yield return null;
        }
        isCycling = false;
        goingForward = !goingForward;
   
    }
 
}
 