using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotRotate : MonoBehaviour
{
    private void Update()
    {
        Quaternion rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        transform.rotation = rotation;
    }
}
