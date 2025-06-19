using UnityEngine;
using UnityEngine.UI;

// 5. UI 매니저 (추가 기능)
public class UIManager : MonoBehaviour
{
    [Header("UI 요소")]
    public Text moneyText; // 플레이어 돈 표시

    private VendingMachine vendingMachine;

    void Start()
    {
        vendingMachine = FindObjectOfType<VendingMachine>();
    }

    void Update()
    {
        // 플레이어 돈 정보 업데이트
        if (moneyText != null && vendingMachine != null)
        {
            moneyText.text = $"보유 코인: {vendingMachine.playerMoney}";
        }
    }
}
