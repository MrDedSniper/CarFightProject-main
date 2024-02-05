using UnityEngine;

[CreateAssetMenu(fileName = "New Product", menuName = "Shop/Product")]
public class ProductData : ScriptableObject
{
    public string productId;
    public int price;
    public Sprite productImage;
}