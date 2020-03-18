using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public float MaxReduction;
    public float MaxIncrease;
    public float RateDamping;
    public float Strength;
    public bool StopFlickering;

    private Light _lightSource;
    private float _baseIntensity;
    private bool _flickering;

    public void Reset()
    {
        MaxReduction = 0.1f;
        MaxIncrease = 0.1f;
        Strength = 300;
    }

    public void Start()
    {
        Reset();

        _lightSource = GetComponent<Light>();
        if (_lightSource == null)
        {
            Debug.LogError("Flicker script must have a Light Component on the same GameObject.");
            return;
        }
        _baseIntensity = _lightSource.intensity;
        StartCoroutine(DoFlicker());
    }

    void Update()
    {
        if (!StopFlickering && !_flickering)
        {
            StartCoroutine(DoFlicker());
        }
    }

    private IEnumerator DoFlicker()
    {
        _flickering = true;
        while (!StopFlickering)
        {
            _lightSource.intensity = Mathf.Lerp(_lightSource.intensity, Random.Range(_baseIntensity - MaxReduction, _baseIntensity + MaxIncrease), Strength * Time.deltaTime);
            yield return new WaitForSeconds(Random.Range(0.1f,0.2f));
        }
        _flickering = false;
    }
}

