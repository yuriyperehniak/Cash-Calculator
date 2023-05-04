using UnityEngine;
using UnityEngine.UI;

public class ClosePopUp : MonoBehaviour
{
    public Button close;
    public GameObject resultPopUp;

    private void Start()
    {
        // ReSharper disable once Unity.NoNullPropagation
        close?.GetComponent<Button>()?.onClick.AddListener(CalculateCash);
    }
    private void CalculateCash()
    {
        resultPopUp.SetActive(false);
    }
}
