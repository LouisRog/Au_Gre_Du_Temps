using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;
    public GameObject twoChoicesButton;
    public GameObject threeChoicesButton;

    static public List<string> stateOfTheGame;

    Message[] currentMessages;
    Message messageToDisplay;
    ChoiceOption choiceToDisplay;
    GameObject buttonSet;
    int activeMessage = 0;

    public void OpenDialogue(Message[] messages, int beginIDMessage)
    {
        currentMessages = messages;
        activeMessage = beginIDMessage;

        Debug.Log("Started conversation ! Loaded messages: " + messages.Length);
        DisplayMessage(ChoiceMessage);
    }

    public void DisplayMessage(UnityEngine.Events.UnityAction ChoiceMessage)
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
            if (messageToDisplay.choices.Length == 3)
            {
                buttonSet = Instantiate(threeChoicesButton, backgroundBox);

                for(int i = 0; i< messageToDisplay.choices.Length; i++)
                {
                    choiceToDisplay = messageToDisplay.choices[i];
                    buttonSet.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = messageToDisplay.choices[i].choice;
                    buttonSet.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(ChoiceMessage);
                }
            }
            
        }
    }

    public void ChoiceMessage()
    {
        Destroy(buttonSet);
        NextMessage(choiceToDisplay.nextMessageId);
        if (choiceToDisplay.newStringState != null)
        {
            stateOfTheGame.Append<string>(choiceToDisplay.newStringState[0]);
        }


    }
    public void NextMessage(int nextMessageId)
    {
        activeMessage = nextMessageId;
        if (activeMessage == 0)
        {
            Debug.Log("Conversation ended");
        }
        else
        {
            DisplayMessage(ChoiceMessage);
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
            NextMessage(messageToDisplay.nextMessageId);
        }
        
    }
}
