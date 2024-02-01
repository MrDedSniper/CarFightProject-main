using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedometrScript : MonoBehaviour
{
   [SerializeField] private CarControls _carControls;
   [SerializeField] private TMP_Text _speedText;

   private void Update()
   {
      _speedText.text = _carControls._currentSpeed.ToString("F0");
   }
}
