using System;
using UnityEngine;
using UnityEngine.UI;

public class VendingMachineButton : MonoBehaviour
{
    public static VendingMachineButton instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }
    }

    [Header("UI 참조")]
    public GameObject purchaseUI; // 구매 UI 패널
    public Text itemNameText; // 아이템 이름 표시
    public Text itemPriceText; // 아이템 가격 표시
    public InputField quantityInput; // 수량 입력 필드
    public Button confirmButton; // 구매 확인 버튼
    public Button cancelButton; // 취소 버튼

    [Header("아이템 정보")]
    public ItemData[] itemDataArray; // 판매할 아이템 데이터 배열
    public int selectedItemIndex = 0; // 현재 선택된 아이템 인덱스

    [Header("버튼 효과")]
    public AudioSource clickSound; // 버튼 클릭 사운드
    public float pressDepth = 0.1f; // 버튼이 눌리는 깊이

    // 새로운 VendingMachineManager 연동
    [Header("새로운 시스템 연동")]
    public VendingMachineManager vendingManager; // 새로운 자판기 매니저
    public bool useNewSystem = false; // 새로운 시스템 사용 여부

    private VendingMachine vendingMachine; // 기존 시스템
    private Vector3 originalPosition;
    private bool isPressed = false;

    void Start()
    {
        vendingMachine = FindObjectOfType<VendingMachine>();
        originalPosition = transform.localPosition;

        // 새로운 시스템 매니저 자동 찾기
        if (vendingManager == null)
            vendingManager = FindObjectOfType<VendingMachineManager>();

        // 이 오브젝트에 Collider가 있는지 확인
        if (GetComponent<Collider>() == null)
        {
            Debug.LogWarning($"{gameObject.name}에 Collider가 없습니다. BoxCollider를 추가하세요.");
        }

        // UI 버튼 이벤트 연결
        if (confirmButton != null) confirmButton.onClick.AddListener(OnConfirmPurchase);
        if (cancelButton != null) cancelButton.onClick.AddListener(OnCancelPurchase);
    }

    public void SaveIndex(int Index)
    {
        selectedItemIndex = Index;

        if (useNewSystem && vendingManager != null)
        {
            // 새로운 시스템: 바로 아이템 생성
            vendingManager.SpawnItem(selectedItemIndex);
        }
        else
        {
            // 기존 시스템: UI 표시
            ShowPurchaseUI();
        }
    }

    // 3D 오브젝트 클릭 감지
    void OnMouseDown()
    {
        if (!isPressed)
        {
            OnButtonPressed();
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

        // UI 표시 (기존 시스템에서만)
        if (!useNewSystem)
        {
            ShowPurchaseUI();
        }
    }

    void OnButtonReleased()
    {
        isPressed = false;

        // 버튼 원래 위치로 복원
        transform.localPosition = originalPosition;
    }

    void ShowPurchaseUI()
    {
        if (purchaseUI != null && itemDataArray != null && itemDataArray.Length > 0)
        {
            purchaseUI.SetActive(true);

            // 선택된 아이템 정보 표시
            ItemData currentItem = itemDataArray[selectedItemIndex];
            if (itemNameText != null) itemNameText.text = currentItem.itemName;
            if (itemPriceText != null) itemPriceText.text = $"가격: {currentItem.price} 코인";
            if (quantityInput != null) quantityInput.text = "1"; // 기본 수량 1
        }
    }

    void OnConfirmPurchase()
    {
        if (quantityInput != null && vendingMachine != null && itemDataArray != null && itemDataArray.Length > 0)
        {
            if (int.TryParse(quantityInput.text, out int quantity) && quantity > 0)
            {
                // 선택된 아이템으로 구매
                vendingMachine.PurchaseItem(itemDataArray[selectedItemIndex], quantity);
                purchaseUI.SetActive(false);
            }
            else
            {
                Debug.Log("올바른 수량을 입력하세요!");
            }
        }
    }

    void OnCancelPurchase()
    {
        if (purchaseUI != null)
        {
            purchaseUI.SetActive(false);
        }
    }

    // 마우스가 버튼 위에 있을 때 하이라이트 효과 (선택사항)
    void OnMouseEnter()
    {
        // 버튼 하이라이트 효과
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.yellow; // 예시: 노란색으로 하이라이트
        }
    }

    void OnMouseExit()
    {
        // 하이라이트 해제
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.white; // 원래 색으로 복원
        }
    }
}