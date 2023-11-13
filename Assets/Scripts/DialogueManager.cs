using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;

    Message[] currentMessages;
    int activeMessage = 0;

    public void OpenDialogue(Message[] messages)
    {
        currentMessages = messages;
        activeMessage = 1;

        Debug.Log("Started conversation ! Loaded messages: " + messages.Length);
        DisplayMessage();
    }

    public void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        Debug.Log(messageToDisplay.message);
        messageText.text = messageToDisplay.message;
        actorName.text = messageToDisplay.actor + " : ";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
