using GoogleUtil;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupStore : PopupComponent
{
    [SerializeField]
    PurchaseMoneyAnimation moneyTxt;

    [SerializeField]
    MoneyTextFrame moneyFrame;

    [Serializable]
    class MenuManage
    {
        public Transform contentsPar;
        public Transform tabPar;

        public Color normalColor;
        public Color pressColor;

        public class MenuData
        {
            [NonSerialized]
            public List<Product> productItemList = new List<Product>();
            public Image imgLine;
            public Text txt;
        }

        public List<MenuData> StoreMenuDataList { get; set; } = new List<MenuData>();

        public int Index { get; set; }

        public void Init()
        {
            StoreMenuDataList.Clear();

            var menuPref = Resources.Load("Prefabs/Store/StoreTab") as GameObject;
            var productPref = Resources.Load("Prefabs/Store/Product") as GameObject;
            foreach (var productDataList in ProductManager.productDic)
            {
                MenuData temp = new MenuData();
                var tab = Instantiate(menuPref, tabPar);
                temp.imgLine = tab.transform.Find("Txt").GetComponent<Image>();
                temp.txt = tab.transform.Find("Line").GetComponent<Text>();
                temp.txt.text = productDataList.Key;

                foreach(var productData in productDataList.Value)
                {
                    var product = Instantiate(productPref, contentsPar).GetComponent<Product>();
                    product.SetProductData(productData);                  
                    temp.productItemList.Add(product);
                }
                StoreMenuDataList.Add(temp);          
            }
        }

        public void OnMenu(int index)
        {
            if (Index != index)
                OffMenu(Index);

            Index = index;

            StoreMenuDataList[Index].imgLine.color = pressColor;
            StoreMenuDataList[Index].txt.color = pressColor;

            var obArr = StoreMenuDataList[Index].productItemList;
            int cnt = obArr.Count;

            for (int i = 0; i < cnt; i++)
            {
                obArr[i].gameObject.SetActive(true);
            }
        }

        public void OffMenu(int index)
        {
            var obArr = StoreMenuDataList[Index].productItemList;
            int cnt = obArr.Count;

            StoreMenuDataList[Index].imgLine.color = normalColor;
            StoreMenuDataList[Index].txt.color = normalColor;

            for (int i = 0; i < cnt; i++)
            {
                obArr[i].gameObject.SetActive(false);
            }
        }

        public void Clear()
        {
            int cnt = StoreMenuDataList.Count;
            for (int i = 0; i < cnt; i++)
            {
                StoreMenuDataList[i].imgLine.color = normalColor;
                StoreMenuDataList[i].txt.color = normalColor;
                foreach (var ob in StoreMenuDataList[i].productItemList)
                {
                    ob.gameObject.SetActive(false);
                }
            }
        }

        public void Refresh()
        {
            var obArr = StoreMenuDataList[Index].productItemList;
            int cnt = obArr.Count;

            for (int i = 0; i < cnt; i++)
            {
                obArr[i].gameObject.SetActive(true);
                obArr[i].GetComponent<Product>().Refresh();
            }
        }
    }

    [SerializeField]
    MenuManage tabManager;

    static public int CacheIndex { get; set; }

    protected void Awake()
    {
        Init();
    }

    protected void OnEnable()
    {
        CacheIndex = 0;
        tabManager.Clear();
        tabManager.OnMenu(CacheIndex);
        ResetAll();
        UIEffectManager.PrintBouncePopup(gameObject, new Vector3(0.8f, 0.8f), 0.5f, 1f, 1f, 0.1f);
    }

    protected void Start()
    {
        ResetAll();
    }

    void Init()
    {
        tabManager.Init();
    }

    public void ResetAll()
    {
        SetCoin();
    }

    public void SetCoin()
    {
        moneyTxt.SetNomalCoinTxt(GlobalData.StringCoin);
        moneyFrame.SetSize(GlobalData.Coin.ToString());
    }

    public void OnMenuClick()
    {
        tabManager.OnMenu(CacheIndex);
    }

    public void RefreshMenuProducts()
    {
        tabManager.Refresh();
    }
}

