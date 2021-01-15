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
        
    }
    public void RecoverBgColor()
    {
        
    }
}
