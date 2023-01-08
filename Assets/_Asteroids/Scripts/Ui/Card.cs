using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public TextMeshProUGUI nameKey;
    public TextMeshProUGUI description;
    public TextMeshProUGUI resource;
    public TextMeshProUGUI cost;
    public Button button;


    public void SetActiveCost(bool isActive)
    {
        resource.enabled = isActive;
        cost.enabled = isActive;
    }
}