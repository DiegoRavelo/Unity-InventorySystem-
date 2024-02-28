using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    void Interact();
}

// public interface IStatisticsManager
// {
//     void ApplyStatsAPlayer(PlayerStatsSO playerStats);
//     
//     void UnApplyStatsAPlayer(PlayerStatsSO playerStats);
//     
//     
// }

public interface IExperenciable
{
    void EventManagerOnExpCollected(int amount);

    void OnEnable();

    void OnDisable();
}
