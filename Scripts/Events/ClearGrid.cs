using UnityEngine;
using UnityEngine.UI;

public class ClearGrid : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.ClearGrid);
        });
    }
}