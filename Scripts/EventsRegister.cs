using UnityEngine;
using UnityEngine.Tilemaps;

public class EventsRegister : MonoBehaviour
{
    public TileBase emptyTile;
    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ClearGrid, () =>
        {
            Tilemap tm = GameObject.Find("Grid/Tilemap").GetComponent<Tilemap>();
            //将所有格子改为empty格子
            for (int y = 1; y < 10; y++)
                for (int x = 1; x < 10; x++)
                    tm.SetTile(new Vector3Int(x, y, 0), emptyTile);
        });
        EventCenter.AddListener(EventDefine.OpenSourceCode, () =>
        {
            Application.OpenURL(@"https://github.com/Sinkorty/Sudoku-Gongge");
        });
        EventCenter.AddListener(EventDefine.SwitchEditMode, () =>
        {
            Switching(true);
        });
        EventCenter.AddListener(EventDefine.SwitchEliminateMode, () =>
        {
            GameObject[] bgs = GameObject.FindGameObjectsWithTag("BG");
            foreach (GameObject bg in bgs)
                bg.GetComponent<SpriteRenderer>().color = new Color(0, 0.7352409f, 1, 1);
            Switching(false);
        });
    }
    private void Switching(bool isEdit)
    {
        //false --> isEdit , true --> !isEdit
        GameObject.Find("Grid").GetComponent<EditGridManager>().enabled = isEdit;
        GameObject.Find("Grid").GetComponent<EliminateManager>().enabled = !isEdit;
        transform.Find("Btn_Eliminate").gameObject.SetActive(!isEdit);
        transform.Find("Tex_PSB_Num").gameObject.SetActive(!isEdit);
        transform.Find("Tex_ABS_Num").gameObject.SetActive(!isEdit);
        transform.Find("Editing").gameObject.SetActive(isEdit);
        transform.Find("Btn_Edit").gameObject.SetActive(!isEdit);
    }
}
