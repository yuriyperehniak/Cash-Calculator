using System.Globalization;
using TMPro;
using UnityEngine;

public class Calculate : MonoBehaviour
{
    public TMP_InputField[] inputFields;
    public TMP_InputField cashDesk;
    private TextMeshProUGUI _summ;
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
        _summ = GameObject.Find("Sum Text").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _sum = 0;
        for (var i = 0; i < inputFields.Length; i++)
        {
            var value = float.Parse(inputFields[i].text);
            var multiplier = _multipliers[i];
            _sum += value * multiplier;
        }

        float.TryParse(cashDesk.text, out _cashDeskValue);
        _summ.text = (_cashDeskValue - _sum).ToString(CultureInfo.CurrentCulture);
    }
}