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
    public TextMeshProUGUI actorMessageText;
    public GameObject actorBackgroundBox;

    public TextMeshProUGUI proprioName;
    public TextMeshProUGUI proprioMessageText;
    public GameObject proprioBackgroundBox;

    public GameObject twoChoicesButton;
    public GameObject threeChoicesButton;
    public Image memoryBackground;
    [SerializeField] GameObject UICanvas;

    private GameObject objectToTrigger;

    static public List<string> stateOfTheGame;

    Message[] currentMessages;
    Message messageToDisplay;
    int activeMessage = 0;

    public void OpenDialogue(Message[] messages, int beginIDMessage)
    {
        currentMessages = messages;
        activeMessage = beginIDMessage;
        UICanvas.SetActive(true);

        actorName.text = "";
        actorMessageText.text = "";

        proprioName.text = "";
        proprioMessageText.text = "";

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
    void DisplayMessageText(Message messageToDisplay)
    {
        if(messageToDisplay.actor == "Propriétaire")
        {
            proprioMessageText.text = messageToDisplay.message;
            proprioName.text = messageToDisplay.actor + " : ";
            proprioBackgroundBox.SetActive(true);
            actorBackgroundBox.SetActive(false);

        }
        else
        {
            Sprite actorImg = Resources.Load<Sprite>("CharacterImages/" + messageToDisplay.actor);
            Sprite memoryImg = Resources.Load<Sprite>("CharacterImages/" + "Proprietaire");
            if (messageToDisplay.backgroundImage != null)
            {
                Debug.Log(messageToDisplay.backgroundImage);
                memoryImg = Resources.Load<Sprite>("FlashbackImages/" + messageToDisplay.backgroundImage);
            }
            actorImage.sprite = actorImg;
            memoryBackground.sprite = memoryImg;
            actorMessageText.text = messageToDisplay.message;
            actorName.text = messageToDisplay.actor + " : ";
            proprioBackgroundBox.SetActive(false);
            actorBackgroundBox.SetActive(true);
        }
    }

    void DisplayMessageChoices(Message messageToDisplay)
    {
        if (messageToDisplay.choices.Length == 3)
        {
            threeChoicesButton.SetActive(true);

            for (int i = 0; i < 3; i++)
            {
                threeChoicesButton.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = messageToDisplay.choices[i].choice;
            }
        }

        if (messageToDisplay.choices.Length == 2)
        {
            twoChoicesButton.SetActive(true);

            for (int i = 0; i < messageToDisplay.choices.Length; i++)
            {
                twoChoicesButton.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = messageToDisplay.choices[i].choice;
            }
        }
    }

    public void DisplayMessage()
    {
        messageToDisplay = currentMessages[activeMessage];

        PlayAudioIfExists(messageToDisplay);

        if (messageToDisplay.choices == null)
        {
            DisplayMessageText(messageToDisplay);
        }
        else
        {
            DisplayMessageChoices(messageToDisplay);
        }
    }

    public void ChoiceMessage(int choiceId)
    {
        threeChoicesButton.SetActive(false);
        twoChoicesButton.SetActive(false);
        if (messageToDisplay.choices[choiceId].newStringState != null)
        {
            for(int i = 0; i< messageToDisplay.choices[choiceId].newStringState.Length; i++)
            {
                stateOfTheGame.Append<string>(messageToDisplay.choices[choiceId].newStringState[i]);
            }
            ObjectStateManager();
        }
        NextMessage(messageToDisplay.choices[choiceId].nextMessageId);


    }
    void ObjectStateManager(){
        foreach(String var in stateOfTheGame){
            objectToTrigger = GameObject.FindWithTag(var);
            objectToTrigger.SetActive(true);
        }
    }

    public void NextMessage(int nextMessageId)
    {
        activeMessage = nextMessageId;
        //Debug.Log(activeMessage);
        if (activeMessage == 0)
        {
            Debug.Log("Conversation ended");
            UICanvas.SetActive(false);
        }
        else
        {
            DisplayMessage();
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        UICanvas.SetActive(false);
        threeChoicesButton.SetActive(false);
        twoChoicesButton.SetActive(false);
        actorBackgroundBox.SetActive(false);
        proprioBackgroundBox.SetActive(false);

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
