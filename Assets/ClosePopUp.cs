using UnityEngine;

public class ClosePopUp : MonoBehaviour
{
    private void Update()
    {
        if (Input.touchCount <= 0) return;
        foreach (var touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Began) continue;
            if (!IsTouchOverPanel(touch.position))
            {
                gameObject.SetActive(false);
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private bool IsTouchOverPanel(Vector2 touchPosition)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, touchPosition);
    }
}