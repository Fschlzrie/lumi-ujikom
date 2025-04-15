using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{
    private Light2D light2D;
    public float minIntensity = 0.6f;
    public float maxIntensity = 1.2f;
    public float flickerSpeed = 0.05f;

    void Start()
    {
        light2D = GetComponent<Light2D>();
        StartCoroutine(Flicker());
    }
    void Update()
    {
        float offsetX = Mathf.PerlinNoise(Time.time * 1.5f, 0f) - 0.5f;
        float offsetY = Mathf.PerlinNoise(0f, Time.time * 1.5f) - 0.5f;
        transform.localPosition = new Vector3(offsetX * 0.1f, offsetY * 0.1f, 0f);
    }


    System.Collections.IEnumerator Flicker()
    {
        while (true)
        {
            light2D.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(flickerSpeed);
        }
    }
}
