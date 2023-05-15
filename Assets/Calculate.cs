using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Calculate : MonoBehaviour
{
    public TMP_InputField[] inputFields;
    public TMP_InputField cashDesk;
    public TextMeshProUGUI cash;
    public TextMeshProUGUI sum;
    public TextMeshProUGUI result;    
    public Button clearButton;
    public Button calculateCash;
    public GameObject resultPopUp;

    private float[] _multipliers;
    private float _sum;
    private float _cashDeskValue;
    private Image _resultPannel;

    private void Start()
    {
        _resultPannel = resultPopUp.GetComponent<Image>();
        _multipliers = new float[inputFields.Length];
        for (var i = 0; i < inputFields.Length; i++)
        {
            float.TryParse(inputFields[i].placeholder.GetComponent<TextMeshProUGUI>().text, NumberStyles.Float, CultureInfo.InvariantCulture, out _multipliers[i]);
        }

        
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
        
        cash.text = "Має бути: " + _cashDeskValue.ToString(CultureInfo.CurrentCulture);
        sum.text = "Наявно: " + _sum.ToString(CultureInfo.CurrentCulture);

        if (_sum > _cashDeskValue)
        {
            result.text = "В касі плюс: " + (_sum - _cashDeskValue).ToString(CultureInfo.CurrentCulture);
            _resultPannel.color = Color.green;
        }
        else if (_sum < _cashDeskValue)
        {
            result.text = "В касі мінус: " + (_sum - _cashDeskValue).ToString(CultureInfo.CurrentCulture);
            _resultPannel.color = Color.red;
        }
        else
        {
            result.text = "Каса зійшлась";
            _resultPannel.color = Color.blue;
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
