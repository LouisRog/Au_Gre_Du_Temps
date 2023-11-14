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
    public RectTransform actorBackgroundBox;

    public TextMeshProUGUI proprioName;
    public TextMeshProUGUI proprioMessageText;

    public GameObject twoChoicesButton;
    public GameObject threeChoicesButton;
    public Image memoryBackground;
    [SerializeField] GameObject UICanvas;

    private GameObject objectToTrigger;

    static public List<string> stateOfTheGame = new List<String>();

    Message[] currentMessages;
    Message messageToDisplay;
    ChoiceOption choiceToDisplay;
    int activeMessage = 0;

    DialogueActivable [] activables;



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
    void DisplayMessageText(Message message)
    {
        if(message.actor == "Propri�taire")
        {
            proprioMessageText.text = messageToDisplay.message;
            proprioName.text = messageToDisplay.actor + " : ";
        }
        else
        {
            Sprite actorImg = Resources.Load<Sprite>("CharacterImages/" + message.actor);
            actorImage.sprite = actorImg;
            actorMessageText.text = messageToDisplay.message;
            actorName.text = messageToDisplay.actor + " : ";
        }
    }

    void DisplayMessageChoices(Message message)
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

        //PlayAudioIfExists(messageToDisplay);

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
        String tag = messageToDisplay.choices[choiceId].newStringState[0];
        if (tag != null)
        {   
            stateOfTheGame.Add(tag);
            ObjectStateManager();
        }
        NextMessage(messageToDisplay.choices[choiceId].nextMessageId);

    }
    void ObjectStateManager(){
        Debug.Log("je lance la marmelade " + activables.Length);
        foreach(var str in stateOfTheGame){
            Debug.Log(str);
            foreach(DialogueActivable da in activables){
                Debug.Log(da);  
                if(da != null && da.gameObject.tag == str)
                    if(str == "removedboat"){
                        da.gameObject.SetActive(false);
                    }
                    else{
                     da.gameObject.SetActive(true);
                    }
            }
           // objectToTrigger = GameObject.FindWithTag(str);
            //objectToTrigger.SetActive(true);
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

        activables = FindObjectsOfType<DialogueActivable>(true);
        Debug.Log("l" + activables.Length);
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
