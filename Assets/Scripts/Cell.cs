using UnityEngine;
using TMPro;

public class Cell : MonoBehaviour
{
    [Header("Apple Data")]
    public int value;
    public bool isSelected;

    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TextMeshPro numberText;

    [Header("Colors")]
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color selectedColor = Color.yellow;

    public void Initialize(int newValue)
    {
        value = newValue;
        numberText.text = value.ToString();
        isSelected = false;
        ResetVisual();
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        spriteRenderer.color = selected ? selectedColor : defaultColor;
    }

    public void ResetVisual()
    {
        isSelected = false;
        spriteRenderer.color = defaultColor;
    }

    public void Remove()
    {
        Destroy(gameObject);
    }

    public Vector2 GetWorldPosition()
    {
        return transform.position;
    }
}