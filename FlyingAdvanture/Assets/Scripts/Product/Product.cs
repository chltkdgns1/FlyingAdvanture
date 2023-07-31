using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoogleUtil
{
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
            // 구매하려고하는 프로덕트 데이터를 넘겨줌
            GoogleIAP.PurchaseState purchaseState = GoogleIAP.PurchaseProduct(productData.productId);
            ShowState(purchaseState);
        }

        void ShowState(GoogleIAP.PurchaseState state)
        {
            switch (state)
            {
                case GoogleIAP.PurchaseState.INITIALIZE_FAILED:
                    //PopupComponent.PopupShow<>();
                    break;
                case GoogleIAP.PurchaseState.NOT_REGISTERED:
                    //PopupComponent.PopupShow<>();
                    break;
                case GoogleIAP.PurchaseState.ALREADY_PURCHASE_STATE:
                    //PopupComponent.PopupShow<>();
                    break;
                default: 
                    // 결제 프로세스 성공
                    break;
            }
        }


        public void Refresh()
        {
            SetData();
        }
    }
}
