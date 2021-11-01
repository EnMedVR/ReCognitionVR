using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    

    public void StartDialogue()
    {
        FindObjectOfType<DialogManager>().OpenDialogue();// messages, actors);
    }
    
    
}

[System.Serializable]
public class Message
{
    public int boxID;
    public int actorID;
    public string message;
}

[System.Serializable]
public class Actor
{
    public string name;
}