using UnityEngine;
using UnityEngine.UI;

public class ReadFile : MonoBehaviour
{
    public static string fileName = string.Empty;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.ReadSudokuFile, fileName);
        });
    }
}
