
using System.Reflection;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction<int> ExpCollected;
    public static void OnExpCollected(int expAmount) => ExpCollected?.Invoke(expAmount);

    public static event UnityAction<ItemSO , FieldInfo[], bool> StatChanged;
    
    public static void OnStatChanged(ItemSO item, FieldInfo [] fields, bool applyStats) => StatChanged?.Invoke(item , fields, applyStats);
}
