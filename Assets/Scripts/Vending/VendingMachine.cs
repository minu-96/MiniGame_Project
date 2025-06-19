using UnityEngine;
using UnityEngine.UI;

public class VendingMachine : MonoBehaviour
{
    [Header("생성 위치")]
    public Transform itemSpawnPoint; // 아이템 상자가 생성될 위치

    [Header("프리팹")]
    public GameObject itemBoxPrefab; // 아이템 상자 프리팹
    public GameObject itemPrefab;

    [Header("플레이어 정보")]
    public int playerMoney = 1000; // 플레이어 보유 코인

    private ItemData a;

    public void PurchaseItem(ItemData itemData, int quantity)
    {
        a = itemData;

        int totalCost = itemData.price * quantity;

        // 돈이 충분한지 확인
        if (playerMoney >= totalCost)
        {
            playerMoney -= totalCost;
            CreateItemBox(itemData, quantity);
            Debug.Log($"{itemData.itemName} {quantity}개 구매 완료!");
        }
        else
        {
            Debug.Log("코인이 부족합니다!");
        }
    }

    void CreateItemBox(ItemData itemData, int quantity)
    {
        // 아이템 상자 생성
        GameObject itemBox = Instantiate(itemBoxPrefab, itemSpawnPoint.position, itemSpawnPoint.rotation);
        GameObject item = Instantiate(itemPrefab, itemSpawnPoint.position, itemSpawnPoint.rotation);

        // 상자에 아이템 정보 설정
        ItemBox boxScript = itemBox.GetComponent<ItemBox>();
        boxScript.SetupBox(itemData, quantity);
    }
}