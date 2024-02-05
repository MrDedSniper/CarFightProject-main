using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScrapUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _scrapText;
    [SerializeField] private ScrapCurrency _scrapCurrency;

    internal void UpdateScrapValue()
    {
        _scrapText.text = _scrapCurrency._currentScrap.ToString(); 
    }
}
