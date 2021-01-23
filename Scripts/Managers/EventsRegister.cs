using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class EventsRegister : MonoBehaviour
{
    Tilemap tm;
    Text tex_log;
    private void Awake()
    {
        tm = GameObject.Find("Grid/Tilemap").GetComponent<Tilemap>();
        tex_log = GameObject.Find("Canvas/Tex_Log").GetComponent<Text>();
        InitEvent();
    }
    private void InitEvent()
    {
        EventCenter.AddListener(EventDefine.ClearGrid, () =>
        {
            //将所有格子改为empty格子
            for (int y = 1; y < 10; y++)
                for (int x = 1; x < 10; x++)
                    tm.SetTile(new Vector3Int(x, y, 0), VarsManager.GetVars().baseTiles[9]);
            GameObject[] bgs = GameObject.FindGameObjectsWithTag("BG");
            foreach (GameObject bg in bgs)
                bg.GetComponent<SpriteRenderer>().color = new Color(0, 0.7352409f, 1, 1);
            GameObject.Find("Canvas/Editing/Btn_EditFinish").SetActive(true);
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
        EventCenter.AddListener(EventDefine.ReadSudokuFile, (string fileName) =>
        {
            EventCenter.Broadcast(EventDefine.ShowLog, "-");
            List<int> nums = new List<int>();
            string caption;
            if (Application.isEditor) caption = ReadFile(@"./" + fileName + ".sd");
            else caption = ReadFile(@"./File Data/" + fileName + ".sd");

            foreach (char c in caption.ToCharArray())   //将文件中的数据转化为全部转化成数字数组
                nums.Add(int.Parse(c.ToString()));
            int i = 0;
            for (int y = 1; y < 10; y++)
                for (int x = 1; x < 10; x++, i++)
                    tm.SetTile(new Vector3Int(x, y, 0), VarsManager.GetVars().frontTiles[nums[i]]);
        });
        EventCenter.AddListener(EventDefine.SaveSudokuFile, (string fileName) =>
        {
            EventCenter.Broadcast(EventDefine.ShowLog, "-");
            StringBuilder caption = new StringBuilder();
            for (int y = 1; y < 10; y++)
                for (int x = 1; x < 10; x++)
                    caption.Append(GetCellNum(new Vector3Int(x, y, 0)));
            if (Application.isEditor) WriteFile(@"./" + fileName + ".sd", caption.ToString());
            else WriteFile(@"./File Data/" + fileName + ".sd", caption.ToString());
        });
        EventCenter.AddListener(EventDefine.ShowLog, (string caption) =>
        {
            tex_log.text += caption;
            if (caption == "-")//"-"为假字符
                tex_log.text = string.Empty;
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
    private string ReadFile(string path)
    {
        try//读取文件，获取内容
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);//为了测试，将文件在C盘桌面上进行读取
            byte[] buffer = new byte[1024];
            int len = file.Read(buffer, 0, buffer.Length);
            string caption = Encoding.ASCII.GetString(buffer, 0, len);
            file.Close();
            file.Dispose();
            return caption;
        }
        catch
        {
            EventCenter.Broadcast(EventDefine.ShowLog, "<color=red>文件读取错误，不存在该文件</color>");
            throw new Exception("文件读取错误，不存在该文件");
        }
    }
    private void WriteFile(string path, string caption)
    {
        try
        {
            FileStream file = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
            byte[] buffer = Encoding.ASCII.GetBytes(caption);
            file.Write(buffer, 0, buffer.Length);
            file.Close();
            file.Dispose();
        }
        catch
        {
            EventCenter.Broadcast(EventDefine.ShowLog, "<color=red>文件存储错误，已有与文件名相同的文件</color>");
            throw new Exception("文件存储错误，已有与文件名相同的文件");
        }
    }
    private int GetCellNum(Vector3Int cellPos)
    {
        string gridName = tm.GetTile(cellPos).name;
        if (gridName != "empty") return int.Parse(gridName);
        else return 0;
    }
}
