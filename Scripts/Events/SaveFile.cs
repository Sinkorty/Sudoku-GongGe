using UnityEngine;
using UnityEngine.UI;

public class SaveFile : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.SaveSudokuFile);
        });
    }
}
