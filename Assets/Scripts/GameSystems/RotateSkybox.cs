using UnityEngine;

public class RotateSkybox : MonoBehaviour
{
    [SerializeField] private Light _directionalLight;
    
    public float duration = 600f;
    public Material skyboxMaterial;
    public float exposureMin;
    public float exposureMax;
    public float intensityMin;
    public float intensityMax;
    public float exposureSpeed = 0.3f;
    public float exposureRandomDelayMin = 0.1f;
    public float exposureRandomDelayMax = 5f; // Изменено на 5

    private float startTime;
    private float exposureStartTime;
    private bool isExposureIncreasing = true;

    private void Start()
    {
        _directionalLight = GameObject.Find("Directional Light").GetComponent<Light>();
        
        exposureMin = Random.Range(1.9f, 2f);
        exposureMax = Random.Range(2.8f, 2.9f);

        startTime = Time.time;
        exposureStartTime = Time.time + Random.Range(exposureRandomDelayMin, exposureRandomDelayMax);
    }

    private void Update()
    {
        // Rotate skybox
        float t = (Time.time - startTime) / duration; 
        float rotation = Mathf.Lerp(0f, 360f, t);
        skyboxMaterial.SetFloat("_Rotation", rotation);

        if (t >= 1f)
        {
            startTime = Time.time;
        }

        // Change exposure
        if (Time.time > exposureStartTime)
        {
            float exposureT = (Time.time - exposureStartTime) / exposureSpeed;
            float exposureValue;

            if (isExposureIncreasing)
            {
                if (exposureT < 0.1f)
                {
                    //Молния
                    exposureValue = Mathf.Lerp(exposureMin, exposureMax * 5f, exposureT / 0.1f);
                    
                    //Интенсивной света
                    float intensity = Mathf.Lerp(0.75f, 1.5f, exposureT / 0.1f);
                    _directionalLight.intensity= intensity;
                }
                else
                {
                    //Молния
                    exposureValue = Mathf.Lerp(exposureMax * 10f, exposureMax, (exposureT - 0.1f) / 0.9f);
                    
                    //Интенсивной света
                    float intensity = Mathf.Lerp(1.5f, 0.75f, (exposureT - 0.1f) / 0.9f);
                    _directionalLight.intensity= intensity;
                }
            }
            else
            {
                // Lightning effect: sharp decrease in brightness
                if (exposureT < 0.1f)
                {
                    //Молния
                    exposureValue = Mathf.Lerp(exposureMax, exposureMin * 10f, exposureT / 0.1f);
                    
                    //Интенсивной света
                    float intensity = Mathf.Lerp(1.5f, 0.75f, exposureT / 0.1f);
                    _directionalLight.intensity = intensity;
                }
                else
                {
                    //Молния
                    exposureValue = Mathf.Lerp(exposureMin * 5f, exposureMin, (exposureT - 0.1f) / 0.9f);
                    
                    //Интенсивной света
                    float intensity = Mathf.Lerp(0.75f, 1.5f, (exposureT - 0.1f) / 0.9f);
                    _directionalLight.intensity = intensity;
                }
            }

            if (exposureT >= 1f)
            {
                isExposureIncreasing = !isExposureIncreasing;
                exposureStartTime = Time.time + Random.Range(exposureRandomDelayMin, exposureRandomDelayMax);
            }

            skyboxMaterial.SetFloat("_Exposure", exposureValue);
        }
    }
}