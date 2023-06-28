using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductManager
{
    static ProductManager instance = new ProductManager();

    //static readonly string[] productId = { "delads", "keythree", "keyfour", "keyfive" };
    static public Dictionary<string , List<ProductData>> productDic = new Dictionary<string, List<ProductData>>();
    static int requestCnt = 0;
   
    ProductManager()
    {
        productDic.Clear();
        UpdateProductData();
    }

    static public void OnStatic() { }

    async void UpdateProductData()
    {
        productDic.Clear();
        await GoogleFirebaseManager.ReadProductData(ResponseProductData);
    }

    async void ResponseProductData(QueryAns ans, object data)
    {
        productDic.Clear();

        var productData = data as Firebase.Database.DataSnapshot;

        if (productData == null)
        {
            requestCnt++;
            if (requestCnt < 10)
                await GoogleFirebaseManager.ReadProductData(ResponseProductData);
            return;
        }

        if (ans != QueryAns.SUCCESS)
        {
            requestCnt++;
            if (requestCnt < 10)
                await GoogleFirebaseManager.ReadProductData(ResponseProductData);
            return;
        }

        foreach (Firebase.Database.DataSnapshot productMenu in productData.Children)
        {
            string menuName = productMenu.Key;

            if (string.IsNullOrEmpty(menuName))
            {
                Debug.LogError($"menuName is null");
                continue;
            }

            productDic.Add(menuName, new List<ProductData>());

            foreach (Firebase.Database.DataSnapshot productChild in productMenu.Children)
            {
                var info = productChild.Child("info");

                if (info == null)
                {
                    Debug.LogError("");
                    continue;
                }
                try 
                { 
                    var infoDic = (IDictionary)info.Value;
                
                    string id = infoDic["id"]?.ToString();
                    string name = infoDic["name"]?.ToString();
                    string mainDescription = infoDic["description"]?.ToString();
                    string subDescription = infoDic["subDescription"]?.ToString();
                    string img = infoDic["img"]?.ToString();
                    string productType = infoDic["ProductType"]?.ToString();
                    string moneyType = infoDic["MoneyType"]?.ToString();
                    string money = infoDic["Money"]?.ToString();

                    ProductData product = new ProductData();
                    product.productId = id;
                    product.productName = name;
                    product.mainDecript = mainDescription;
                    product.subDescript = subDescription;
                    product.img = img;
                    product.productType = (ProductTypes)int.Parse(productType);
                    product.moneyType = (MoneyType)int.Parse(moneyType);
                    product.money = long.Parse(money);
                    productDic[menuName].Add(product);
                }
                catch(Exception e)
                {
                    Debug.LogError($"ResponseProductData error : {e.Message}\n Stack : {e.StackTrace}");
                    continue;
                }
            }            
        }
    }
}
