using UnityEngine;
using UnityEngine.UI;

public class ReadFile : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.ReadSudokuFile);
        });
    }
}
