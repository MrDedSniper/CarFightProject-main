using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WarningsScripts : MonoBehaviour
{
    [SerializeField] private GameObject _cantFindRoom;
    [SerializeField] private GameObject _notEnoughScrap;
    [SerializeField] private float _fadeDuration = 2f;
    private float _startAlpha = 100f;
    
    private Coroutine _fadeCoroutine;
    
    internal void CantFindRoomWarning()
    {
        ShowWarning(_cantFindRoom);
    }
    
    internal void NotEnoughScrapWarning()
    {
        ShowWarning(_notEnoughScrap);
    }

    private void ShowWarning(GameObject warningObject)
    {
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
        }

        Image panelImage = warningObject.GetComponentInChildren<Image>();
        Color startColor = panelImage.color;
        startColor.a = _startAlpha / 255f;
        panelImage.color = startColor;
        
        TMP_Text text = warningObject.GetComponentInChildren<TMP_Text>();
        Color textColor = text.color;
        textColor.a = _startAlpha / 255f;
        text.color = textColor;

        warningObject.SetActive(true);
        _fadeCoroutine = StartCoroutine(FadeOut(warningObject));
    }
    
    private IEnumerator FadeOut(GameObject warningObject)
    {
        Image panelImage = warningObject.GetComponentInChildren<Image>();
        TMP_Text text = warningObject.GetComponentInChildren<TMP_Text>();
        
        float startAlpha = _startAlpha / 255f;
        float endAlpha = 0f;
        float startTime = Time.time;
        
        while (panelImage.color.a > endAlpha || text.color.a > endAlpha)
        {
            float t = (Time.time - startTime) / _fadeDuration;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, t);

            Color newPanelColor = panelImage.color;
            newPanelColor.a = alpha;
            panelImage.color = newPanelColor;

            Color newTextColor = text.color;
            newTextColor.a = alpha;
            text.color = newTextColor;

            yield return null;
        }

        warningObject.SetActive(false);
    }
}