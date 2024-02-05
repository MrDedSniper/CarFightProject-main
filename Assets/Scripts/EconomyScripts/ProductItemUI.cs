using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductItemUI : MonoBehaviour
{
    [SerializeField] private Image _productImage;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Image _soldImage;
    [SerializeField] private Button _buyButton;

    public void SetProductData(ProductData product)
    {
        _productImage.sprite = product.productImage;
        _priceText.text = product.price.ToString() + " SC";
    }

    public void ItemSold(ProductData product)
    {
        _soldImage.enabled = true;
        _productImage.color = new Color(1f, 1f, 1f, 0.5f); // Устанавливаем полупрозрачность для изображения продукта
        _priceText.color = new Color(1f, 1f, 1f, 0.5f); // Устанавливаем полупрозрачность для текста цены
        _buyButton.interactable = false; // Делаем кнопку покупки неактивной
    }
}