using UnityEngine;
using UnityEngine.UI;

public class SwitchEditMode : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.SwitchEditMode);
        });
    }
}
