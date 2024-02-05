using UnityEngine;
using UnityEngine.UI;

public class PrefabButtonController : MonoBehaviour
{
    public ShopBehavior ShopBehavior;
    public ProductData productData; // Добавляем поле для хранения информации о продукте

    private void Start()
    {
        ShopBehavior = FindObjectOfType<ShopBehavior>();
        
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (productData != null) // Проверяем, что информация о продукте установлена
        {
            ShopBehavior.TryToBuyItem(productData); // Передаем информацию о продукте в метод TryToBuyItem
        }
        else
        {
            Debug.LogError("Product data is not set for this button!");
        }
    }
}