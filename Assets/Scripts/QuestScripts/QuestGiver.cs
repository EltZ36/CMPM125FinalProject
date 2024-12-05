using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using TMPro;
using UnityEngine;

//from https://www.youtube.com/watch?v=e7VEe_qW4oE at 6:20 to 10:27
public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    public PlayerController player;

    public Canvas questWindow;
    public TMP_Text tileText;
    public TMP_Text descriptionText;


    // Start is called before the first frame update
    public void OpenQuestWindow()
    {
        questWindow.gameObject.SetActive(true);
        tileText.text = quest.title;
        descriptionText.text = quest.description;
    }

    public void AcceptQuest()
    {
        quest.isActive = true;
        quest.completed = false;
        player.currentQuest = quest;
    }
}
