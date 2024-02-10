using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Image _parallax1;
    [SerializeField] private Image _parallax2;
    [SerializeField] private Image _parallax3;
    [SerializeField] private Image _blackImageBG;
    
    private Vector3 _startPosition1;
    private Vector3 _endPosition1;
    private Vector3 _startPosition2;
    private Vector3 _endPosition2;
    private Vector3 _startPosition3;
    private Vector3 _endPosition3;
    private float _speed1 = 5f;
    private float _speed2 = 2f;
    private float _speed3 = 4f;

    private void Start()
    {
        _startPosition1 = new Vector3(0, 450, 0);
        _endPosition1 = new Vector3(0, 150, 0);

        _startPosition2 = new Vector3(150, 0, 0);
        _endPosition2 = new Vector3(750, 0, 0);

        _startPosition3 = new Vector3(-1100, 0, 0);
        _endPosition3 = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        MoveToPosition(_parallax1, _startPosition1, _endPosition1, _speed1);
        MoveToPosition(_parallax2, _startPosition2, _endPosition2, _speed2);
        MoveToPosition(_parallax3, _startPosition3, _endPosition3, _speed3);
    }

    private void MoveToPosition(Image image, Vector3 startPosition, Vector3 endPosition, float speed)
    {
        float distance = Vector3.Distance(image.rectTransform.localPosition, endPosition);
        float duration = distance / speed;
        float t = Mathf.Min(1, Time.deltaTime / duration);
        
        image.rectTransform.localPosition = Vector3.Lerp(image.rectTransform.localPosition, endPosition, t);

        if (Vector3.Distance(image.rectTransform.localPosition, endPosition) < 0.1f)
        {
            Vector3 temp = startPosition;
            startPosition = endPosition;
            endPosition = temp;
        }
    }

    private void TurnOnBlackImage()
    {
        _blackImageBG.gameObject.SetActive(true);
    }
    
}
