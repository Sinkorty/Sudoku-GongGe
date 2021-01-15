using System;
using UnityEngine;
using UnityEngine.Tilemaps;

//[CreateAssetMenu(fileName = "Vars Container")]
public class VarsManager : ScriptableObject
{
    public TileBase[] frontTiles = new TileBase[9];
    public TileBase[] baseTiles = new TileBase[10];
    [HideInInspector]
    public Vector3Int[] dires ={
        Vector3Int.zero,//自身
        Vector3Int.left,//左
        Vector3Int.left + Vector3Int.up,//左上
        Vector3Int.up,//上
        Vector3Int.up + Vector3Int.right,//右上
        Vector3Int.right,//右
        Vector3Int.right + Vector3Int.down,//右下
        Vector3Int.down,//下
        Vector3Int.down + Vector3Int.left,//左下
    };//方向顺序
    public static VarsManager GetVars()
    {
        return Resources.Load<VarsManager>("Vars Container");
    }
}
