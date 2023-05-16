using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveInputData : MonoBehaviour
{
    public TMP_InputField[] inputFields;
    public Button clearButton;
    public Button yes;
    public Button no;
    public GameObject сonfirmationPopUp;
    
    private string[] _inputFieldKeys;

    private void Start()
    {
        InitializeInputFieldKeys();
        LoadInputData();

        clearButton.onClick.AddListener(SetActive);
        yes.onClick.AddListener(ClearInputData);
        no.onClick.AddListener(SetActive);
    }

    private void Update()
    {
        SaveData();
    }

    private void SetActive()
    {
        сonfirmationPopUp.SetActive(!сonfirmationPopUp.activeSelf);
    }
    
    private void InitializeInputFieldKeys()
    {
        _inputFieldKeys = new string[inputFields.Length];

        for (var i = 0; i < inputFields.Length; i++)
        {
            _inputFieldKeys[i] = "InputData" + (i + 1);
        }
    }

    private void SaveData()
    {
        for (var i = 0; i < inputFields.Length; i++)
        {
            var inputData = inputFields[i].text;
            PlayerPrefs.SetString(_inputFieldKeys[i], inputData);
        }
        PlayerPrefs.Save();
    }

    private void LoadInputData()
    {
        for (var i = 0; i < inputFields.Length; i++)
        {
            if (!PlayerPrefs.HasKey(_inputFieldKeys[i])) continue;
            var savedInputData = PlayerPrefs.GetString(_inputFieldKeys[i]);
            inputFields[i].text = savedInputData;
        }
    }

    private void ClearInputData()
    {
        for (var i = 0; i < inputFields.Length; i++)
        {
            PlayerPrefs.DeleteKey(_inputFieldKeys[i]);
            inputFields[i].text = "";
        }
    }
}