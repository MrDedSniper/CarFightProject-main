using UnityEngine;
using UnityEngine.UI;

public class UIElementHandler : MonoBehaviour
{
    private UISoundsManager audioManager;

    void Start()
    {
        audioManager = GetComponent<UISoundsManager>();

        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            if (button.gameObject.tag == "UISingleButton")
            {
                button.onClick.AddListener(() => audioManager.PlayButtonSound());
            }
        }

        InputField[] inputFields = FindObjectsOfType<InputField>();
        foreach (InputField inputField in inputFields)
        {
            if (inputField.gameObject.tag == "UIInputField")
            {
                inputField.onEndEdit.AddListener((value) => audioManager.PlayInputFieldSound());
            }
        }
    }
}