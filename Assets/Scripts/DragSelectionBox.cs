using UnityEngine;
using UnityEngine.UI;

public class DragSelectionBox : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform selectionBox;

    private RectTransform canvasRect;
    private Vector2 startLocalPos;
    private bool isDragging;

    private void Awake()
    {
        canvasRect = canvas.GetComponent<RectTransform>();

        if (selectionBox != null)
        {
            selectionBox.gameObject.SetActive(false);
            selectionBox.pivot = new Vector2(0.5f, 0.5f);
        }
    }

    private void Update()
    {
        // 마우스 왼쪽 클릭 시작
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            selectionBox.gameObject.SetActive(true);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                Input.mousePosition,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
                out startLocalPos
            );

            selectionBox.anchoredPosition = startLocalPos;
            selectionBox.sizeDelta = Vector2.zero;
        }

        // 드래그 중
        if (isDragging && Input.GetMouseButton(0))
        {
            Vector2 currentLocalPos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                Input.mousePosition,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
                out currentLocalPos
            );

            UpdateSelectionBox(startLocalPos, currentLocalPos);
        }

        // 드래그 종료
        if (isDragging && Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            // 필요하면 여기서 선택 판정 처리
            selectionBox.gameObject.SetActive(false);
        }
    }

    private void UpdateSelectionBox(Vector2 start, Vector2 end)
    {
        Vector2 center = (start + end) * 0.5f;
        Vector2 size = new Vector2(
            Mathf.Abs(end.x - start.x),
            Mathf.Abs(end.y - start.y)
        );

        selectionBox.anchoredPosition = center;
        selectionBox.sizeDelta = size;
    }
}