using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendingMachineManager : MonoBehaviour
{
    public GameObject box;
    [Header("자판기 설정")]
    public Transform itemSpawnPoint; // 아이템이 나올 위치
    public AudioSource audioSource; // 오디오 소스
    public AudioClip buttonClickSound; // 버튼 클릭 소리
    public AudioClip itemDropSound; // 아이템 떨어지는 소리

    [Header("아이템 프리팹 리스트")]
    public List<GameObject> itemPrefabs = new List<GameObject>(); // 아이템 프리팹들

    [Header("아이템 생성 설정")]
    public float dropForce = 5f; // 아이템 떨어지는 힘
    public float spawnDelay = 0.5f; // 아이템 생성 딜레이

    [Header("효과 설정")]
    public ParticleSystem sparkleEffect; // 반짝이 효과 (선택사항)

    private bool isProcessing = false; // 아이템 생성 중인지 체크

    void Start()
    {
        // 오디오 소스가 없으면 자동으로 추가
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        // 스폰 포인트가 설정되지 않았으면 현재 위치 사용
        if (itemSpawnPoint == null)
            itemSpawnPoint = transform;
    }

    // 버튼에서 호출할 메인 메서드
    public void SpawnItem(int itemIndex)
    {
        if (isProcessing) return; // 이미 처리 중이면 무시

        StartCoroutine(SpawnItemCoroutine(itemIndex));
    }

    private IEnumerator SpawnItemCoroutine(int itemIndex)
    {
        isProcessing = true;

        // 버튼 클릭 소리 재생
        PlaySound(buttonClickSound);

        // 효과 재생 (있을 경우)
        if (sparkleEffect != null)
            sparkleEffect.Play();

        // 딜레이
        yield return new WaitForSeconds(spawnDelay);

        // 아이템 생성
        CreateItem(itemIndex);

        // 아이템 떨어지는 소리 재생
        PlaySound(itemDropSound);

        isProcessing = false;
    }

    private void CreateItem(int itemIndex)
    {
        // 인덱스 유효성 검사
        if (itemIndex < 0 || itemIndex >= itemPrefabs.Count)
        {
            Debug.LogWarning($"잘못된 아이템 인덱스: {itemIndex}");
            return;
        }

        // 아이템 프리팹이 null인지 확인
        if (itemPrefabs[itemIndex] == null)
        {
            Debug.LogWarning($"아이템 프리팹이 null입니다. 인덱스: {itemIndex}");
            return;
        }

        // 아이템 생성
        GameObject newItem = Instantiate(itemPrefabs[itemIndex], itemSpawnPoint.position, itemSpawnPoint.rotation);

        GameObject newItem1 = Instantiate(box, itemSpawnPoint.position, itemSpawnPoint.rotation);
        Rigidbody rb1 = newItem1.GetComponent<Rigidbody>();
        if (rb1 == null)
            rb1 = newItem.AddComponent<Rigidbody>();
        rb1.AddForce(Vector3.down * dropForce, ForceMode.Impulse);
        rb1.AddTorque(Random.insideUnitSphere * 2f, ForceMode.Impulse);

        // 아이템에 물리 효과 추가
        Rigidbody rb = newItem.GetComponent<Rigidbody>();
        if (rb == null)
            rb = newItem.AddComponent<Rigidbody>();

        // 아래쪽으로 힘 가하기
        rb.AddForce(Vector3.down * dropForce, ForceMode.Impulse);

        // 약간의 랜덤 회전 추가
        rb.AddTorque(Random.insideUnitSphere * 2f, ForceMode.Impulse);

        Debug.Log($"아이템 생성됨: {itemPrefabs[itemIndex].name}");
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // 아이템 추가 메서드 (런타임에서 아이템 추가 가능)
    public void AddItem(GameObject itemPrefab)
    {
        itemPrefabs.Add(itemPrefab);
    }

    // 아이템 제거 메서드
    public void RemoveItem(int index)
    {
        if (index >= 0 && index < itemPrefabs.Count)
        {
            itemPrefabs.RemoveAt(index);
        }
    }

    // 현재 아이템 개수 반환
    public int GetItemCount()
    {
        return itemPrefabs.Count;
    }
}
