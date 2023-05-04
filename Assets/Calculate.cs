using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Calculate : MonoBehaviour
{
    public TMP_InputField[] inputFields;
    public TMP_InputField cashDesk;
    private TextMeshProUGUI _summ;
    private GameObject _clear;
    private float[] _multipliers;
    private float _sum;
    private float _cashDeskValue;

    private void Start()
    {
        _multipliers = new float[inputFields.Length];
        for (var i = 0; i < inputFields.Length; i++)
        {
            var placeholderText = inputFields[i].text;
            float.TryParse(placeholderText, out _multipliers[i]);
        }
        // ReSharper disable once Unity.NoNullPropagation
        _clear?.GetComponent<Button>()?.onClick.AddListener(ClearInput);
        _summ = GameObject.Find("Sum Text").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _sum = 0;
        for (var i = 0; i < inputFields.Length; i++)
        {
            if (!float.TryParse(inputFields[i].text, out var value)) continue;
            var multiplier = _multipliers[i];
            _sum += value * multiplier;
        }

        float.TryParse(cashDesk.text, out _cashDeskValue);
        _summ.text = _sum.ToString(CultureInfo.InvariantCulture);
        // _summ.text = (_cashDeskValue - _sum).ToString(CultureInfo.CurrentCulture);
    }

    private void ClearInput()
    {
        cashDesk.text = "";
        foreach (var inputField in inputFields)
        {
            inputField.text = "";
        }
    }
}