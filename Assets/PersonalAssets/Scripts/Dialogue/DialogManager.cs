using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    //public Image actorImage;
    public Text[] actorName;
    public Text[] messageText;
    public RectTransform[] backgroundBox;

    public Message[] currentMessages;
    public Actor[] currentActors;
    int activeMessage = 0;
    int panelID = 0;
    public static bool isActive = false;

    public Panel[] panelList;
    public AudioSource audioSource;
    public AudioClip[] NPCAudio;

    string currentDate = System.DateTime.Now.ToString("f");

    public void OpenDialogue()//Message[] messages, Actor[] actors)
    {
        //currentMessages = messages;
        //currentActors = actors;
        if (currentMessages.Length == 15)
        {
            currentMessages[4].message = currentMessages[4].message + currentDate; //Adds date and time to the message about date and time
        }
        
        activeMessage = 0;
        isActive = true;
        Debug.Log("Started conversation! Loaded messages: " + currentMessages.Length);
        DisplayMessage();
    }

    void DisplayMessage()
    {
        Debug.Log(System.DateTime.Now.ToString("HH:mm dd MMMM, yyyy"));
        Debug.Log(activeMessage);
        // Gets the current message (with boxID)
        Message messageToDisplay = currentMessages[activeMessage];
        panelID = messageToDisplay.boxID;
        
        messageText[messageToDisplay.boxID].text = messageToDisplay.message;

        // Sets the actor from the current message to the correct text box
        Actor actorToDisplay = currentActors[messageToDisplay.actorID];
        actorName[messageToDisplay.boxID].text = actorToDisplay.name;

        // Setup Panel
        FindObjectOfType<MenuManager>().SetCurrentWithHistory(panelList[panelID]);
        audioSource.PlayOneShot(NPCAudio[activeMessage]);
        //actorImage.sprite = actorToDisplay.sprite;
    }
    
    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
            Debug.Log("Active Message = " + activeMessage);
        }
        else
        {
            //Debug.Log("Conversation ended!");
            isActive = false;
        }
    }

    /*public void PanelCheckAdvance()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            Debug.Log("Not at end of message chain");
        }
        else
        {
            FindObjectOfType<MenuManager>().SetCurrentWithHistory(panelList[0]); ;
        }
    }*/

    public void PreviousMessage()
    {
        activeMessage--;
        if (activeMessage >= 0)
        {
            DisplayMessage();
            //Debug.Log("Active Message = " + activeMessage);
        }
        else
        {
            if (activeMessage < -1) //Resets the activeMessage counter
            {
                activeMessage = 0;
            }
            //Debug.Log("Active Message = " + activeMessage);
            isActive = false;
        }
    }

}
