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

        //Debug.Log("Started conversation ! Loaded messages: " + messages.Length);
        DisplayMessage();
    }
    void PlayAudioIfExists(Message message)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioClip foundClip = Resources.Load<AudioClip>("FlashbackSounds/" + message.messageId.ToString());
        if (foundClip != null)
        {
            audioSource.clip = foundClip;
            audioSource.Play();
        }
    }
    
    public void DisplayMessage()
    {
        messageToDisplay = currentMessages[activeMessage];

        PlayAudioIfExists(messageToDisplay);

        if (messageToDisplay.choices == null)
        {
            messageText.text = messageToDisplay.message;
            actorName.text = messageToDisplay.actor + " : ";
        }
        else
        {
            Debug.Log(messageToDisplay.choices.Length);
            if (messageToDisplay.choices.Length == 3)
            {
                buttonSet = Instantiate(threeChoicesButton, backgroundBox);

                for(int i = 0; i< messageToDisplay.choices.Length; i++)
                {
                    choiceToDisplay = messageToDisplay.choices[i];
                    int nextMessage = choiceToDisplay.nextMessageId;
                    buttonSet.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = messageToDisplay.choices[i].choice;
                    buttonSet.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(()=> ChoiceMessage(nextMessage));
                }
            }

            if (messageToDisplay.choices.Length == 2)
            {
                buttonSet = Instantiate(twoChoicesButton, backgroundBox);

                for (int i = 0; i < messageToDisplay.choices.Length; i++)
                {
                    choiceToDisplay = messageToDisplay.choices[i];
                    int nextMessage = choiceToDisplay.nextMessageId;
                    buttonSet.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = messageToDisplay.choices[i].choice;
                    buttonSet.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(() => ChoiceMessage(nextMessage));
                }
            }

        }
    }

    public void ChoiceMessage(int nextMessageId)
    {
        Destroy(buttonSet);
        NextMessage(nextMessageId);
        if (choiceToDisplay.newStringState != null)
        {
            stateOfTheGame.Append<string>(choiceToDisplay.newStringState[0]);
        }


    }
    public void NextMessage(int nextMessageId)
    {
        activeMessage = nextMessageId;
        //Debug.Log(activeMessage);
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
            NextMessage(messageToDisplay.nextMessageId);
        }
        //Debug.Log(stateOfTheGame);
        
    }
}
