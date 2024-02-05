using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class ShopBehavior : MonoBehaviour
{
   [SerializeField] private ScrapCurrency _scrapCurrency;
   [SerializeField] private ProductItemUI _productItemUI;
   [SerializeField] private WarningsScripts _warningsScripts;
   
   public List<ProductData> productsList;
   public GameObject productItemPrefab;
   public Transform contentPanel;

   private void Awake()
   {
      _scrapCurrency = FindObjectOfType<ScrapCurrency>();
   }

   private void Start()
   {
      foreach (ProductData product in productsList)
      {
         GameObject productItem = Instantiate(productItemPrefab, contentPanel);
         ProductItemUI productItemUI = productItem.GetComponent<ProductItemUI>();
         productItemUI.SetProductData(product);
      }
   }

   public void TryToBuyItem(ProductData product)
   {
      if (_scrapCurrency._currentScrap >= product.price)
      {
         BuyItem(product);
      }
      else
      {
         _warningsScripts.NotEnoughScrapWarning();
      }
   }

   private void BuyItem(ProductData product)
   {
      var request = new PurchaseItemRequest
      {
         ItemId = product.productId,
         VirtualCurrency = "SC", // Название виртуальной валюты
         Price = product.price // Цена товара
      };

      Debug.Log("ID " + product.productId + ", Цена " + product.price);
      
      PlayFabClientAPI.PurchaseItem(request, OnPurchaseSuccess, OnPurchaseFailure);
      
      ProductData purchasedProduct = productsList.Find(p => p.productId == product.productId);// Найти купленный товар по ID
      _productItemUI.ItemSold(purchasedProduct);
   }
   
   private void OnPurchaseSuccess(PurchaseItemResult result)
   {
      Debug.Log("Покупка успешна!");
   }

   private void OnPurchaseFailure(PlayFabError error)
   {
      Debug.LogError("Ошибка покупки: " + error.ErrorMessage);
   }
}
