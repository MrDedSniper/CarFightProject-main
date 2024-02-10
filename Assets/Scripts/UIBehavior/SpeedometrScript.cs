using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class SpeedometrScript : MonoBehaviour
{
   [SerializeField] private TMP_Text _speedText;
   
   public CarController _carControls;
   public PhotonView _photonView;

   private void Start()
   {
      _photonView = GetComponent<PhotonView>();
      
      GameObject playerCarObject = GameObject.FindGameObjectWithTag("PlayerCar");
      
      if (playerCarObject != null)
      {
         _carControls = playerCarObject.GetComponent<CarController>();
      }
      else
      {
         Debug.LogError("Object with tag 'PlayerCar' not found");
      }

      if (!_photonView.IsMine)
      {
         enabled = false;
      }
   }

   private void Update()
   {
      if (_photonView.IsMine && _carControls != null)
      {
         _speedText.text = _carControls._currentSpeed.ToString("F0");
      }
   }
}
