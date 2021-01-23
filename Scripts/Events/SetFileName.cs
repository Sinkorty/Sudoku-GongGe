using UnityEngine;
using UnityEngine.UI;

public class SetFileName : MonoBehaviour
{
    private void Awake()
    {
        InputField inputField = GetComponent<InputField>();
        inputField.onEndEdit.AddListener((string caption) =>
        {
            ReadFile.fileName = inputField.text;
            SaveFile.fileName = inputField.text;
        });
    }
}
