using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//quest class was done with https://www.youtube.com/watch?v=e7VEe_qW4oE at 4:00 to 4:47
[System.Serializable]
public class Quest
{
    public bool isActive;
    public bool completed;
    public string title;
    public string description;
    public QuestGoal goal;

    public void Complete()
    {
        isActive = false;
        completed = true;
    }
}
