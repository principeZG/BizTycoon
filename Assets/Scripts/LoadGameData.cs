using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameData : MonoBehaviour
{
    public delegate void LoadDataComplete();
    public static event LoadDataComplete OnLoadDataComplete;


    public TextAsset GameData;
    public GameObject StorePrefab;
    public GameObject StorePanel;
    
    public void Start()
    {
        LoadData();
        if (OnLoadDataComplete != null)
        {
            OnLoadDataComplete();
        }

    }
    public void LoadData()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(GameData.text);
        XmlNodeList storeList = xmlDoc.GetElementsByTagName("store");

        foreach (XmlNode storeInfo in storeList )
        {

            // Create a instance (duplicate) object of a Store
            GameObject newStore = (GameObject) Instantiate(StorePrefab);
            
            // Referencing to a script component "Store" of a instatiated newStore object
            Store storeObj = newStore.GetComponent<Store>();

            XmlNodeList storeNodes = storeInfo.ChildNodes;
            foreach (XmlNode storeNode in storeNodes)
            {
                Debug.Log(storeNode.Name);
                Debug.Log(storeNode.InnerText);

                if (storeNode.Name == "Name")
                {
                        storeObj.StoreName.text = storeNode.InnerText;
                }
                if (storeNode.Name == "BaseStoreCost")
                {
                    storeObj.BaseStoreCost = float.Parse(storeNode.InnerText);
                }
                if (storeNode.Name == "BaseStoreProfit")
                {
                    storeObj.BaseStoreProfit = float.Parse(storeNode.InnerText);
                }
                if (storeNode.Name == "StoreTimer")
                {
                    storeObj.StoreTimer = float.Parse(storeNode.InnerText);
                }
                if (storeNode.Name == "Image")
                {
                    Sprite newSprite = Resources.Load<Sprite>(storeNode.InnerText);
                    Image storeImage = newStore.transform.Find("ImageButtonCollectProfits").GetComponent<Image>();

                    storeImage.sprite = newSprite;
                }
                if (storeNode.Name == "StoreMultiplier")
                {
                    storeObj.StoreMultiplier = float.Parse(storeNode.InnerText);
                }
                if (storeNode.Name == "StoreTimerDivision")
                {
                    storeObj.StoreTimerDivision = int.Parse(storeNode.InnerText);
                }
                if (storeNode.Name == "StoreCount")
                {
                    storeObj.StoreCount = int.Parse(storeNode.InnerText);
                }
            }

            storeObj.SetNextStoreCost(storeObj.BaseStoreCost);
            newStore.transform.SetParent(StorePanel.transform);
        }
    }

}
