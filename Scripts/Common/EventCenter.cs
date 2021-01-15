using System;
using System.Collections.Generic;
/// <summary>
/// 事件中心，用于添加、广播、删除事件，一个事件码可添加多个同种参数的事件（它们存在同一个委托）
/// </summary>
public class EventCenter
{
    private static Dictionary<EventDefine, Delegate> m_EventTable = new Dictionary<EventDefine, Delegate>();
    /// <summary> 事件表 </summary>
    public static Dictionary<EventDefine, Delegate> M_EventTable { get { return m_EventTable; } }
    private static void OnListenerAdding(EventDefine ed, Delegate cb)
    {
        if (!m_EventTable.ContainsKey(ed))
        {
            m_EventTable.Add(ed, null);
        }
        Delegate d = m_EventTable[ed];
        if (d != null && d.GetType() != cb.GetType())
        {
            throw new Exception(string.Format("无法为事件{0}添加不同类型的委托，当前事件对应的委托为{1}，要添加的委托类型为{2}", ed, d.GetType(), cb.GetType()));
        }
    }
    private static void OnListenerRemoving(EventDefine ed, Delegate cb)
    {
        if (m_EventTable.ContainsKey(ed))
        {
            Delegate d = m_EventTable[ed];
            if (d == null) throw new Exception(string.Format("移除监听错误：事件{0}未定义任何委托", ed));
            else if (d.GetType() != cb.GetType()) throw new Exception(string.Format("移除监听错误：无法为事件{0}移除不同类型的委托，事件的委托类型为：{1}；\n要移除的委托类型为：{2}", ed, d.GetType(), cb.GetType()));
        }
        else
        {
            throw new Exception(string.Format("移除监听错误，未定义事件码{0}", ed));
        }
    }
    public static void AddListener(EventDefine ed, CallBack cb)
    {
        OnListenerAdding(ed, cb);
        m_EventTable[ed] = (CallBack)m_EventTable[ed] + cb;
    }
    public static void AddListener<T>(EventDefine ed, CallBack<T> cb)
    {
        OnListenerAdding(ed, cb);
        m_EventTable[ed] = (CallBack<T>)m_EventTable[ed] + cb;
    }
    public static void AddListener<T, X>(EventDefine ed, CallBack<T, X> cb)
    {
        OnListenerAdding(ed, cb);
        m_EventTable[ed] = (CallBack<T, X>)m_EventTable[ed] + cb;
    }
    public static void AddListener<T, X, Y>(EventDefine ed, CallBack<T, X, Y> cb)
    {
        OnListenerAdding(ed, cb);
        m_EventTable[ed] = (CallBack<T, X, Y>)m_EventTable[ed] + cb;
    }
    public static void AddListener<T, X, Y, Z>(EventDefine ed, CallBack<T, X, Y, Z> cb)
    {
        OnListenerAdding(ed, cb);
        m_EventTable[ed] = (CallBack<T, X, Y, Z>)m_EventTable[ed] + cb;
    }
    public static void RemoveListener(EventDefine ed, CallBack cb)
    {
        OnListenerRemoving(ed, cb);
        m_EventTable[ed] = (CallBack)m_EventTable[ed] - cb;
        if (m_EventTable[ed] == null) m_EventTable.Remove(ed);
    }
    public static void RemoveListener<T>(EventDefine ed, CallBack<T> cb)
    {
        OnListenerRemoving(ed, cb);
        m_EventTable[ed] = (CallBack<T>)m_EventTable[ed] - cb;
        if (m_EventTable[ed] == null) m_EventTable.Remove(ed);
    }
    public static void RemoveListener<T, X>(EventDefine ed, CallBack<T, X> cb)
    {
        OnListenerRemoving(ed, cb);
        m_EventTable[ed] = (CallBack<T, X>)m_EventTable[ed] - cb;
        if (m_EventTable[ed] == null) m_EventTable.Remove(ed);
    }
    public static void RemoveListener<T, X, Y>(EventDefine ed, CallBack<T, X, Y> cb)
    {
        OnListenerRemoving(ed, cb);
        m_EventTable[ed] = (CallBack<T, X, Y>)m_EventTable[ed] - cb;
        if (m_EventTable[ed] == null) m_EventTable.Remove(ed);
    }
    public static void RemoveListener<T, X, Y, Z>(EventDefine ed, CallBack<T, X, Y, Z> cb)
    {
        OnListenerRemoving(ed, cb);
        m_EventTable[ed] = (CallBack<T, X, Y, Z>)m_EventTable[ed] - cb;
        if (m_EventTable[ed] == null) m_EventTable.Remove(ed);
    }
    public static void Broadcast(EventDefine ed)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(ed, out d))
        {
            CallBack cb = d as CallBack;
            if (cb != null) cb();
            else throw new Exception(string.Format("广播事件错误：事件{0}对应的委托类型不同", ed));
        }
    }
    public static void Broadcast<T>(EventDefine ed, T arg)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(ed, out d))
        {
            CallBack<T> cb = d as CallBack<T>;
            if (cb != null) cb(arg);
            else throw new Exception(string.Format("广播事件错误：事件{0}对应的委托类型不同", ed));
        }
    }
    public static void Broadcast<T, X>(EventDefine ed, T arg1, X arg2)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(ed, out d))
        {
            CallBack<T, X> cb = d as CallBack<T, X>;
            if (cb != null) cb(arg1, arg2);
            else throw new Exception(string.Format("广播事件错误：事件{0}对应的委托类型不同", ed));
        }
    }
    public static void Broadcast<T, X, Y>(EventDefine ed, T arg1, X arg2, Y arg3)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(ed, out d))
        {
            CallBack<T, X, Y> cb = d as CallBack<T, X, Y>;
            if (cb != null) cb(arg1, arg2, arg3);
            else throw new Exception(string.Format("广播事件错误：事件{0}对应的委托类型不同", ed));
        }
    }
    public static void Broadcast<T, X, Y, Z>(EventDefine ed, T arg1, X arg2, Y arg3, Z arg4)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(ed, out d))
        {
            CallBack<T, X, Y, Z> cb = d as CallBack<T, X, Y, Z>;
            if (cb != null) cb(arg1, arg2, arg3, arg4);
            else throw new Exception(string.Format("广播事件错误：事件{0}对应的委托类型不同", ed));
        }
    }
}