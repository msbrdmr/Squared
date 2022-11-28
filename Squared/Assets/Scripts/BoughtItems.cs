using UnityEngine;

public class BoughtItems : MonoBehaviour
{
    [System.Serializable]
    public struct BoughtGridItem
    {
        public string name;
        public Sprite icon;
    }
    public BoughtGridItem[] boughtitemarray;
    public GameObject BoughtgridView;
    private void Start()
    {
        BoughtgridView.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (boughtitemarray.Length > 0)
        {
            BoughtgridView.gameObject.SetActive(true);


        }
    }

}
