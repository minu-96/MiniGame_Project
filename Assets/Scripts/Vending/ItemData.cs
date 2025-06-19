// 2. 아이템 데이터 구조
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string itemName;
    public int price;
    public GameObject itemPrefab; // 실제 아이템 프리팹
    public Sprite itemIcon; // 아이템 아이콘
}