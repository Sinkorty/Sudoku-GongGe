public enum EventDefine
{
    Eliminate,              //排除 0
    ClearGrid,              //清除九宫格 0
    SwitchEditMode,         //切换为编辑模式 0
    SwitchEliminateMode,    //切换为排除模式 0
    OpenSourceCode,         //打开Github项目的源代码 0
    HideFinishEdit,         //隐藏"完成编辑" 0
    ReadSudokuFile,         //读取数独文件 0
    SaveSudokuFile,         //存储数独文件 0
    GetCentralPos,          //获取格子对应宫的中心坐标 1
    ShowLog,                //显示log信息 1
}