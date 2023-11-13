using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;
    public Button choicePrefab;

    Message[] currentMessages;
    Message messageToDisplay;
    int activeMessage = 0;

    public void OpenDialogue(Message[] messages, int beginIDMessage)
    {
        currentMessages = messages;
        activeMessage = beginIDMessage;

        Debug.Log("Started conversation ! Loaded messages: " + messages.Length);
        DisplayMessage();
    }

    public void DisplayMessage()
    {
        messageToDisplay = currentMessages[activeMessage];
        if (messageToDisplay.choices == null)
        {
            Debug.Log(messageToDisplay.message);
            messageText.text = messageToDisplay.message;
            actorName.text = messageToDisplay.actor + " : ";
        }
        else
        {
            for(int i = 0; i< messageToDisplay.choices.Length; i++)
            {
                Button b = Instantiate(choicePrefab);
                b.transform.SetParent(backgroundBox.transform);
                b.GetComponentInChildren<TextMeshProUGUI>().text = messageToDisplay.choices[i].choice;

            }
            
        }
    }

    public void NextMessage(Message currentMessage)
    {
        Debug.Log(activeMessage);
        activeMessage = currentMessage.nextMessageId;
        Debug.Log(activeMessage);
        if (activeMessage == 0)
        {
            Debug.Log("Conversation ended");
        }
        else
        {
            DisplayMessage();
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextMessage(messageToDisplay);
        }
        
    }
}
