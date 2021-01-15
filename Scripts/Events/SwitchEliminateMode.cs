using UnityEngine;
using UnityEngine.UI;

public class SwitchEliminateMode : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.SwitchEliminateMode);
        });
    }
}
