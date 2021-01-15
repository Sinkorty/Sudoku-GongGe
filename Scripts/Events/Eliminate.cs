using UnityEngine;
using UnityEngine.UI;

public class Eliminate : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.Eliminate);
        });
    }
}
