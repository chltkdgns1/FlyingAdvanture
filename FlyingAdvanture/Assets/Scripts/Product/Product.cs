using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Product : MonoBehaviour
{
    public Image mainImg;
    public Text mainTxt;
    public Text subTxt;
    public Text moneyTxt;
    public Image moneyImg;

    public Transform lockBack;

    private ProductData productData;

    private bool isLock = false;

    public void SetProductData(ProductData data)
    {
        productData = data;
        SetData();
    }

    void SetData()
    {
        mainImg.sprite = AtlasManager.Instance.purchaseAtals.GetSprite(productData.img);
        mainTxt.text = productData.mainDecript;
        subTxt.text = productData.subDescript;

        if (productData.moneyType == MoneyType.MONEY)
        {
            moneyTxt.text = productData.money.ToString() + " 원";
            moneyImg.sprite = AtlasManager.Instance.lobbyAtlas.GetSprite("cristal_b");
        }
        else
        {
            moneyTxt.text = productData.money.ToString() + " 골드";
            moneyImg.sprite = AtlasManager.Instance.lobbyAtlas.GetSprite("coin_02");
        }

        if (lockBack != null)
        {
            if (productData.isLock)
                lockBack.gameObject.SetActive(true);
            else
                lockBack.gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {

    }

    public void Refresh()
    {
        SetData();
    }
}
