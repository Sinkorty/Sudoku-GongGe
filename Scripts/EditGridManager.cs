using UnityEngine;
using UnityEngine.Tilemaps;

public class EditGridManager : MonoBehaviour
{
    public TileBase[] frontTiles;
    public GameObject editFinished;
    public static Tilemap tm;
    private GameObject[] bgs;
    private void Awake()
    {
        tm = transform.GetChild(0).gameObject.GetComponent<Tilemap>();
        bgs = GameObject.FindGameObjectsWithTag("BG");
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3Int cellPos = GetComponent<EliminateManager>().GetCellPos();
            if (cellPos != Vector3Int.zero)//鼠标在格子上
            {
                int num = GetComponent<EliminateManager>().GetTileNum(cellPos);
                if (num == -1) num = 0;
                num++;
                if (num == 10) num = 0;
                if (num == -1) num = 9;
                tm.SetTile(cellPos, frontTiles[num]);
                JudgeGrid();
            }
        }
        if (Input.mouseScrollDelta.y != 0)//滑动鼠标滚轮
        {
            Vector3Int cellPos = GetComponent<EliminateManager>().GetCellPos();
            if (cellPos != Vector3Int.zero)//鼠标在格子上
            {
                int num = GetComponent<EliminateManager>().GetTileNum(cellPos);
                if (num == -1) num = 0;
                num += Mathf.CeilToInt(Input.mouseScrollDelta.y);
                if (num == 10) num = 0;
                if (num == -1) num = 9;
                tm.SetTile(cellPos, frontTiles[num]);
                JudgeGrid();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector3Int cellPos = GetComponent<EliminateManager>().GetCellPos();
            if (cellPos != Vector3Int.zero)
            {
                tm.SetTile(cellPos, frontTiles[0]);
                JudgeGrid();
            }
        }
    }
    /// <summary> 判断整个宫格是否正确 </summary>
    private void JudgeGrid()
    {
        for (int y = 1; y < 10; y++)
            for (int x = 1; x < 10; x++)
            {
                if (!JudgeCell(new Vector3Int(x, y, 0)))//编辑错误
                {
                    editFinished.SetActive(false);
                    foreach (GameObject bg in bgs) bg.GetComponent<SpriteRenderer>().color = Color.red;//背景颜色变红
                    return;
                }
            }
        foreach (GameObject bg in bgs) bg.GetComponent<SpriteRenderer>().color = Color.green;//背景颜色变绿
        editFinished.SetActive(true);
    }
    /// <summary> 判断该格子的编辑是否正确 </summary>
    /// <returns> 如果编辑错误，返回false </returns>
    private bool JudgeCell(Vector3Int cellPos)
    {
        if (GetTileNum(cellPos) == -1)
            return true;
        int num = GetTileNum(cellPos);
        //从行判断
        for (int x = 1; x < 10; x++)
            if (x != cellPos.x && GetTileNum(new Vector3Int(x, cellPos.y, 0)) == num)
            {
                //print("Edit error with line,CellPos:" + cellPos);
                return false;
            }
        //从列判断
        for (int y = 1; y < 10; y++)
            if (y != cellPos.y && GetTileNum(new Vector3Int(cellPos.x, y, 0)) == num)
            {
                //print("Edit error with list,CellPos:" + cellPos);
                return false;
            }
        //从宫判断
        Vector3Int centrolPos = EliminateManager.GetCentrolPos(cellPos);
        foreach (Vector3Int dire in EliminateManager.dires)
        {
            if (centrolPos + dire != cellPos && GetTileNum(centrolPos + dire) == num)
            {
                //print("Edit error with Gong,CellPos:" + cellPos);
                return false;
            }
        }
        return true;
    }
    private int GetTileNum(Vector3Int ceilPos)
    {
        string gridName = tm.GetTile(ceilPos).name;//通过坐标获取格子的名称
        if (gridName != "empty")//当前格子不是空格子
            return int.Parse(gridName);//返回格子所带有的数字
        else
            return -1;//返回假值-1
    }
    private void OnDisable()
    {
        GetComponent<EliminateManager>().InstanceGrid();
    }
}
