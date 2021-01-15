using UnityEngine;
using UnityEngine.Tilemaps;

public class EventsManager : MonoBehaviour
{
    public TileBase empty;
    GameObject[] bgs;
    private void Awake()
    {
        bgs = GameObject.FindGameObjectsWithTag("BG");
    }
    public void RunSudokuGongge()
    {
        Application.OpenURL("https://github.com/Sinkorty/Sudoku-Gongge");
    }
    public void ClearGrid()
    {
        //将所有格子改为empty格子
        for (int y = 1; y < 10; y++)
            for (int x = 1; x < 10; x++)
                EditGridManager.tm.SetTile(new Vector3Int(x, y, 0), empty);
    }
    public void RecoverBgColor()
    {
        foreach (GameObject bg in bgs)
            bg.GetComponent<SpriteRenderer>().color = new Color(0, 0.7352409f, 1, 1);
    }
}
