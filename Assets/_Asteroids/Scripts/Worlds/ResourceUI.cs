using TMPro;
using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI value;

    public void SetValue(string newValue)
    {
        value.text = newValue;
    }

    public void SetTitle(string resourceName)
    {
        title.text = resourceName;
    }
}