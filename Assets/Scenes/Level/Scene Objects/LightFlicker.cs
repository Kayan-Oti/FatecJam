using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    public List<Light2D> myLights;
    public float maxInterval = 1;
    public float maxFlicker = 0.2f;

    private List<float> defaultIntensities = new List<float>();
    private bool isOn;
    private float timer;
    private float delay;

    private void Start()
    {
        foreach(Light2D light in myLights)
            defaultIntensities.Add(light.intensity);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay)
        {
            ToggleLight();
        }
    }

    void ToggleLight()
    {
        isOn = !isOn;

        if (isOn)
        {
            for(int i=0;i<myLights.Count;i++)
                myLights[i].intensity = defaultIntensities[i];
            
            delay = Random.Range(0, maxInterval);
        }
        else
        {
            float randomPerCent = Random.Range(0,1);
            for(int i=0;i<myLights.Count;i++)
                myLights[i].intensity = defaultIntensities[i]*randomPerCent;
            delay = Random.Range(0, maxFlicker);
        }

        timer = 0;
    }
}
