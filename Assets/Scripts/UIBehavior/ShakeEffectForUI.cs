using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShakeEffectForUI : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 targetPosition = new Vector3(30, 30, 0);
    private float shakeDuration = 1f;
    private float shakeMagnitude = 0.1f;

    void Start()
    {
        initialPosition = transform.position;
    }

    internal void StartShake()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        Debug.Log("Shake");
        
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            transform.position = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        yield return new WaitForSeconds(1f); // Ждем 1 секунду

        elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            transform.position = targetPosition + Random.insideUnitSphere * shakeMagnitude;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = initialPosition;
    }
}
