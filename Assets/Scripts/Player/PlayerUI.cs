using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pickUpUI;

    [SerializeField] public TextMeshProUGUI currentText;
    [SerializeField] public GameObject currentObject;

    public ScriptableObject currentItem;

    // Start is called before the first frame update
    void Start()
    {
        currentObject = pickUpUI.gameObject;
        currentText = pickUpUI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
