using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreItems : MonoBehaviour
{
    [System.Serializable]
    public struct GridItemArray
    {
        public string name;
        public int price;
        public Sprite icon;
        public Button.ButtonClickedEvent clickevent;
        public GameObject gridItem;
        public Button gridButton;
        public TMP_Text tmp;

    }
    public GridItemArray[] storeItems;

    public GameObject StoregridView;

    public GameObject gridItemPrefab;

    void Start()
    {
        for (int i = 0; i < storeItems.Length; i++)
        {
            storeItems[i].gridItem = Instantiate(gridItemPrefab);
            storeItems[i].gridItem.transform.SetParent(StoregridView.transform);
            storeItems[i].gridButton = storeItems[i].gridItem.GetComponentInChildren<Button>();
            storeItems[i].tmp = storeItems[i].gridItem.GetComponentInChildren<TMP_Text>();
            storeItems[i].tmp.text = storeItems[i].name+" "+ storeItems[i].price +"$" ;
            storeItems[i].gridButton.image.sprite = storeItems[i].icon;
            storeItems[i].gridButton.onClick = storeItems[i].clickevent;

        }
    }
}
