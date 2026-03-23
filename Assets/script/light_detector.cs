using System.Linq;
using Lumi;
using UnityEngine;

public class light_detector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LightDetector light_detector = GetComponent<LightDetector>();
        Light[] lights = GameObject.FindGameObjectWithTag("lights").GetComponentsInChildren<Light>();
        light_detector.lights = lights.ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
