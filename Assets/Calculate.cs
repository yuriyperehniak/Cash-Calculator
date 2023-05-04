using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Calculate : MonoBehaviour
{
    public TMP_InputField[] inputFields;
    public TMP_InputField cashDesk;
    public TextMeshProUGUI result;
    public Button clearButton;
    public Button calculateCash;
    public GameObject resultPopUp;

    private float[] _multipliers;
    private float _sum;
    private float _cashDeskValue;

    private void Start()
    {
        _multipliers = new float[inputFields.Length];
        for (var i = 0; i < inputFields.Length; i++)
        {
            float.TryParse(inputFields[i].placeholder.GetComponent<TextMeshProUGUI>().text, NumberStyles.Float, CultureInfo.InvariantCulture, out _multipliers[i]);
        }

        result.text = "";
        
        // ReSharper disable once Unity.NoNullPropagation
        calculateCash?.GetComponent<Button>()?.onClick.AddListener(CalculateCash);
        clearButton.onClick.AddListener(ClearInput);
    }

    private void Update()
    {
        _sum = 0;
        for (var i = 0; i < inputFields.Length; i++)
        {
            if (float.TryParse(inputFields[i].text, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
            {
                _sum += value * _multipliers[i];
            }
        }

        float.TryParse(cashDesk.text, NumberStyles.Float, CultureInfo.InvariantCulture, out _cashDeskValue);

        if (_sum > _cashDeskValue)
        {
            result.text = "В касі плюс: " + (_sum - _cashDeskValue).ToString(CultureInfo.CurrentCulture);
        }
        else if (_sum < _cashDeskValue)
        {
            result.text = "В касі мінус: " + (_sum - _cashDeskValue).ToString(CultureInfo.CurrentCulture);
        }
        else
        {
            result.text = "Каса зійшлась";
        }
    }

    private void ClearInput()
    {
        cashDesk.text = "";
        foreach (var inputField in inputFields)
        {
            inputField.text = "";
        }

        result.text = "";
    }

    private void CalculateCash()
    {
        Update();
        resultPopUp.SetActive(true);
    }
}
