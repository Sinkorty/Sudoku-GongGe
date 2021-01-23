using UnityEngine;
using UnityEngine.UI;

public class SaveFile : MonoBehaviour
{
    public static string fileName = string.Empty;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.SaveSudokuFile, fileName);
        });
    }
}
