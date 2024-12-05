using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//code is from https://www.youtube.com/watch?v=e7VEe_qW4oE to 11:26 to 18:08
[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void Summon()
    {
        if(goalType == GoalType.Summon)
        {
            currentAmount++;
        }
    }
}

public enum  GoalType
{
    Summon
}