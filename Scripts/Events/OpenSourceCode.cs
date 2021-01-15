using UnityEngine;
using UnityEngine.UI;

public class OpenSourceCode : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.OpenSourceCode);
        });
    }
}
