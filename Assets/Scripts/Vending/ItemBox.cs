using UnityEngine;
using UnityEngine.UI;

// 4. 아이템 상자 스크립트
public class ItemBox : MonoBehaviour
{
    [Header("상자 설정")]
    public GameObject boxMesh; // 상자 메시
    public Transform itemContainer; // 아이템들이 들어갈 컨테이너

    [Header("아이템 생성 설정")]
    public float itemSpacing = 0.3f; // 아이템 간 간격
    public float dropHeight = 0.5f; // 아이템이 떨어질 높이
    public int maxItemsPerRow = 5; // 한 줄에 최대 아이템 수

    private ItemData itemData;
    private int quantity;
    private bool isOpened = false;

    public void SetupBox(ItemData data, int qty)
    {
        itemData = data;
        quantity = qty;
    }

    void OnMouseDown()
    {
        if (!isOpened)
        {
            OpenBox();
        }
    }

    void OpenBox()
    {
        isOpened = true;

        // 상자 메시 비활성화
        if (boxMesh != null)
        {
            boxMesh.SetActive(false);
        }

        // 아이템들 생성
        //CreateItems();

        // 마법 효과 추가 (선택사항)
        PlayOpenEffect();
    }
    /*
    void CreateItems()
    {
        // itemData는 하나의 아이템 정보 (예: 지팡이 데이터)
        // quantity는 그 아이템의 개수 (예: 지팡이 1개)

        if (itemData == null)
        {
            Debug.LogError("아이템 데이터가 null입니다!");
            return;
        }

        if (itemData.itemPrefab == null)
        {
            Debug.LogError($"아이템 프리팹이 설정되지 않았습니다: {itemData.itemName}");
            return;
        }

        Debug.Log($"{itemData.itemName} 프리팹으로 {quantity}개 생성 시작");

        // 선택된 아이템을 quantity 개수만큼 생성
        for (int i = 0; i < quantity; i++)
        {
            // 아이템 생성 위치 계산 (격자 배치)
            Vector3 itemPosition = CalculateItemPosition(i);
            Vector3 worldPosition = transform.position + itemPosition;

            Debug.Log($"아이템 {i + 1} 생성 위치: {worldPosition}");

            // 같은 아이템 프리팹을 여러 개 생성
            GameObject item = Instantiate(itemData.itemPrefab, worldPosition, Random.rotation);

            if (item == null)
            {
                Debug.LogError($"아이템 생성 실패: {itemData.itemName}");
                continue;
            }

            Debug.Log($"아이템 생성 성공: {item.name}");

            // 아이템을 컨테이너의 자식으로 설정 (컨테이너가 있을 경우)
            if (itemContainer != null)
            {
                item.transform.SetParent(itemContainer);
            }

            // 아이템에 CollectibleItem 스크립트 추가 (없다면)
            if (item.GetComponent<CollectibleItem>() == null)
            {
                CollectibleItem collectible = item.AddComponent<CollectibleItem>();
                collectible.itemData = itemData; // 같은 아이템 데이터 할당
                Debug.Log($"CollectibleItem 스크립트 추가됨: {item.name}");
            }

            // 아이템에 물리 효과 추가 (떨어지는 효과)
            AddPhysicsToItem(item, i);
        }

        Debug.Log($"{itemData.itemName} {quantity}개 생성 완료!");
    }

    Vector3 CalculateItemPosition(int index)
    {
        // 격자 배치 계산
        int row = index / maxItemsPerRow;
        int col = index % maxItemsPerRow;

        // 중앙 정렬을 위한 오프셋 계산
        int itemsInThisRow = Mathf.Min(maxItemsPerRow, quantity - (row * maxItemsPerRow));
        float rowOffset = (itemsInThisRow - 1) * itemSpacing * 0.5f;

        return new Vector3(
            (col * itemSpacing) - rowOffset,
            dropHeight + (row * 0.1f), // 약간씩 높이 차이
            row * itemSpacing * 0.5f
        );
    }

    void AddPhysicsToItem(GameObject item, int index)
    {
        // Rigidbody 추가 또는 가져오기
        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = item.AddComponent<Rigidbody>();
        }

        // 랜덤한 힘 추가 (아이템이 자연스럽게 흩어지도록)
        Vector3 randomForce = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(0.5f, 2f),
            Random.Range(-1f, 1f)
        );

        rb.AddForce(randomForce, ForceMode.Impulse);

        // 회전력도 추가
        Vector3 randomTorque = Random.insideUnitSphere * 5f;
        rb.AddTorque(randomTorque, ForceMode.Impulse);

        // 일정 시간 후 물리 효과 비활성화 (성능 최적화)
        StartCoroutine(DisablePhysicsAfterTime(rb, 3f));
    }

    System.Collections.IEnumerator DisablePhysicsAfterTime(Rigidbody rb, float time)
    {
        yield return new WaitForSeconds(time);

        if (rb != null)
        {
            rb.isKinematic = true; // 물리 시뮬레이션 비활성화
        }
    }
    */
    void PlayOpenEffect()
    {
        // 파티클 효과나 사운드 재생
        ParticleSystem effect = GetComponent<ParticleSystem>();
        if (effect != null)
        {
            effect.Play();
        }

        // 사운드 재생
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.Play();
        }
    }
}