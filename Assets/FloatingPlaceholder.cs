using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class FloatingPlaceholder : MonoBehaviour
{
    [Header("Floated Settings")]
    [SerializeField] private Vector3 floatedPosition = new Vector3(3, -1.5f, 0);
    [SerializeField] private float floatedSize = 5f;
    [SerializeField] private TextAlignmentOptions floatedAlignment = TextAlignmentOptions.TopLeft;

    [Header("Animation")]
    [SerializeField] private float speed = 8f;

    private TMP_InputField inputField;
    private TextMeshProUGUI placeholder;

    private Vector3 normalPosition;
    private float normalSize;
    private TextAlignmentOptions normalAlignment;
    private bool isFloated;

    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();

        // шукаємо плейсхолдер серед дітей
        placeholder = null;
        foreach (var tmp in inputField.GetComponentsInChildren<TextMeshProUGUI>(true))
        {
            if (tmp.gameObject.name.ToLower().Contains("placeholder"))
            {
                placeholder = tmp;
                break;
            }
        }

        if (placeholder == null)
        {
            Debug.LogWarning($"[{name}] FloatingPlaceholder: Placeholder не знайдено!");
            return;
        }

        // зберігаємо початкові параметри
        normalPosition = placeholder.transform.localPosition;
        normalSize = placeholder.fontSize;
        normalAlignment = placeholder.alignment;

        // початковий стан
        isFloated = !string.IsNullOrEmpty(inputField.text);

        // підписка на зміни тексту
        inputField.onValueChanged.AddListener(OnTextChanged);

        // Вимикаємо автоматичне керування TMP
        inputField.placeholder = null;
    }

    private void OnTextChanged(string text)
    {
        isFloated = !string.IsNullOrEmpty(text);
        // не чіпаємо SetActive, плейсхолдер завжди активний
    }

    private void Update()
    {
        if (placeholder == null) return;

        // плавний розмір
        float targetSize = isFloated ? floatedSize : normalSize;
        placeholder.fontSize = Mathf.Lerp(placeholder.fontSize, targetSize, Time.deltaTime * speed);

        // плавна позиція
        Vector3 targetPos = isFloated ? floatedPosition : normalPosition;
        placeholder.transform.localPosition = Vector3.Lerp(placeholder.transform.localPosition, targetPos, Time.deltaTime * speed);

        // alignment змінюється одразу
        placeholder.alignment = isFloated ? floatedAlignment : normalAlignment;
    }
}
