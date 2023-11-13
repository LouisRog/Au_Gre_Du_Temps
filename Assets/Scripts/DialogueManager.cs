using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public TextMeshPro actorName;
    public TextMeshPro messageText;
    public RectTransform backgroundBox;

    List<Message> currentMessages;
    int activeMessage = 0;

    public void OpenDialogue(List<Message> messages)
    {
        currentMessages = messages;
        activeMessage = 0;
    }

    public void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;
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
