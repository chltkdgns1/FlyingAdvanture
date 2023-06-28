using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProductTypes
{
    DeleteAdsGoogle,
    UnLockStageThree,
    UnLockStageFour,
    UnLockStageFive,

    // 구글 상품이 아님
    NonGoolgeProduct_DeleteAds, // 해당 enum type 은 제일 하단에 위치함. 구글 상품이 아님
}

public enum MoneyType
{
    MONEY,
    INGAMEMONEY
}
public class ProductData
{
    public string productId;
    public string productName;
    public string mainDecript;
    public string subDescript;
    public string img;
    public long money;
    public ProductTypes productType;
    public MoneyType moneyType;
    public bool isLock = false;
}