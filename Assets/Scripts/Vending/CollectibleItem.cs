using UnityEngine;
using UnityEngine.UI;

// 6. 개별 아이템 스크립트 (수집 가능하게 만들기)
public class CollectibleItem : MonoBehaviour
{
    [Header("아이템 정보")]
    public ItemData itemData;
    public float collectRange = 2f; // 수집 가능 거리

    private bool canCollect = false;

    void Start()
    {
        // 생성 후 잠시 기다린 후 수집 가능하게 설정
        Invoke("EnableCollection", 1f);
    }

    void EnableCollection()
    {
        canCollect = true;
    }

    void OnMouseDown()
    {
        if (canCollect)
        {
            CollectItem();
        }
    }

    void CollectItem()
    {
        // 인벤토리에 아이템 추가 로직
        Debug.Log($"{itemData.itemName} 수집!");

        // 수집 효과
        PlayCollectEffect();

        // 아이템 제거
        Destroy(gameObject);
    }

    void PlayCollectEffect()
    {
        // 수집 시 효과 (파티클, 사운드 등)
        // 아이템이 플레이어에게 빨려들어가는 효과 등
    }
}
