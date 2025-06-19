using System;
using UnityEngine;
using UnityEngine.UI;

public class OtherItem : MonoBehaviour
{
    [Header("인덱스 설정")]
    public int Index = 0; // 이 버튼의 아이템 인덱스

    [Header("버튼 효과")]
    public AudioSource clickSound; // 버튼 클릭 사운드
    public float pressDepth = 0.1f; // 버튼이 눌리는 깊이

    [Header("시스템 선택")]
    public bool useNewSystem = false; // 새로운 시스템 사용 여부
    public VendingMachineManager vendingManager; // 새로운 시스템 매니저

    private VendingMachine vendingMachine; // 기존 시스템
    private Vector3 originalPosition;
    private bool isPressed = false;
    private Renderer buttonRenderer;
    private Color originalColor;

    void Start()
    {
        // 기존 시스템 연결
        vendingMachine = FindObjectOfType<VendingMachine>();

        // 새로운 시스템 매니저 자동 찾기
        if (vendingManager == null)
            vendingManager = FindObjectOfType<VendingMachineManager>();

        // 초기 설정
        originalPosition = transform.localPosition;
        buttonRenderer = GetComponent<Renderer>();
        if (buttonRenderer != null)
            originalColor = buttonRenderer.material.color;

        // Collider 확인
        if (GetComponent<Collider>() == null)
        {
            Debug.LogWarning($"{gameObject.name}에 Collider가 없습니다. BoxCollider를 추가하세요.");
        }
    }

    void OnMouseDown()
    {
        if (!isPressed)
        {
            OnButtonPressed();

            if (useNewSystem && vendingManager != null)
            {
                // 새로운 시스템: 바로 아이템 생성
                vendingManager.SpawnItem(Index);
                Debug.Log($"새로운 시스템: 아이템 인덱스 {Index} 생성 요청");
            }
            else if (VendingMachineButton.instance != null)
            {
                // 기존 시스템: VendingMachineButton을 통해 인덱스 전달
                VendingMachineButton.instance.SaveIndex(Index);
                Debug.Log($"기존 시스템: 아이템 인덱스 {Index} 전달");
            }
            else
            {
                Debug.LogWarning("VendingMachineButton instance를 찾을 수 없습니다!");
            }
        }
    }

    void OnMouseUp()
    {
        if (isPressed)
        {
            OnButtonReleased();
        }
    }

    void OnButtonPressed()
    {
        isPressed = true;

        // 버튼 눌리는 애니메이션
        transform.localPosition = originalPosition + Vector3.down * pressDepth;

        // 클릭 사운드 재생
        if (clickSound != null)
        {
            clickSound.Play();
        }
    }

    void OnButtonReleased()
    {
        isPressed = false;

        // 버튼 원래 위치로 복원
        transform.localPosition = originalPosition;
    }

    // 마우스가 버튼 위에 있을 때 하이라이트 효과
    void OnMouseEnter()
    {
        if (buttonRenderer != null)
        {
            buttonRenderer.material.color = Color.yellow; // 하이라이트 색상
        }
    }

    void OnMouseExit()
    {
        if (buttonRenderer != null)
        {
            buttonRenderer.material.color = originalColor; // 원래 색으로 복원
        }
    }

    // 런타임에서 인덱스 변경 메서드
    public void SetIndex(int newIndex)
    {
        Index = newIndex;
        Debug.Log($"{gameObject.name}의 인덱스가 {newIndex}로 변경되었습니다.");
    }

    // 시스템 전환 메서드
    public void ToggleSystem()
    {
        useNewSystem = !useNewSystem;
        Debug.Log($"{gameObject.name}이 {(useNewSystem ? "새로운" : "기존")} 시스템으로 전환되었습니다.");
    }

    // 새로운 시스템으로 설정
    public void SetToNewSystem()
    {
        useNewSystem = true;
        if (vendingManager == null)
            vendingManager = FindObjectOfType<VendingMachineManager>();
    }

    // 기존 시스템으로 설정
    public void SetToOldSystem()
    {
        useNewSystem = false;
    }
}
