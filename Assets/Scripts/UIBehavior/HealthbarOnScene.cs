using UnityEngine;
using UnityEngine.UI;

public class HealthbarOnScene : MonoBehaviour
{
    [SerializeField] private Slider _healthbarSlider;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    public void UpdateHealtbar(float currentHP, float maxHP)
    {
        _healthbarSlider.value = currentHP / maxHP;
    }
    private void Update()
    {
        Quaternion rotation = _camera.transform.rotation;
        rotation.x = 0f;
        rotation.z = 0f;
        transform.rotation = rotation;
        
        Camera camera = (Camera) FindObjectOfType(typeof(Camera));
        if (camera)
        {
            transform.LookAt(camera.gameObject.transform);
        }
        
        transform.position = _target.position + _offset;
    }
}
