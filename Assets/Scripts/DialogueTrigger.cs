using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    public int next_conversation_index = 0;
    static public bool noMoreConversation = false;
    [SerializeField]
    public int[] conversations_entry_points = { 1, 51, 101, 151, 201 };

    private Message[] messages;
    public string[] stringStates;
    [SerializeField] GameObject DialogueBox;

    void Start()
    {
        messages = new Message[1000]; // Make sure its big enough for our proto

        TextAsset[] jsonFiles = Resources.LoadAll<TextAsset>("Json/");
        List<Message> allMessages = new List<Message>();

        foreach (TextAsset filePath in jsonFiles)
        {
            string json = filePath.text;
            MessageList data = JsonUtility.FromJson<MessageList>(json);
            foreach (Message d in data.messages) {
                allMessages.Add(d);
            }
        }

        foreach (Message m in allMessages)
        {
            messages[m.messageId] = m;
        }

        Debug.Log("Loaded this many messages: " + allMessages.Count);
    }


    public void ClickOnDoor()
    {
        if (next_conversation_index < conversations_entry_points.Length)
        {
            DialogueBox.GetComponent<DialogueManager>().OpenDialogue(messages, conversations_entry_points[next_conversation_index]);
            next_conversation_index++;
            if (next_conversation_index == 5){
                noMoreConversation = true;
            }
        }
    }
    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }
}

public class MessageList {
    public Message[] messages;
}

[System.Serializable]
public class Message
{
    public string actor;
    public int messageId;
    public int nextMessageId;
    public string expression;
    public string message;
    public ChoiceOption[] choices;
    public string backgroundImage;
}

[System.Serializable]
public class ChoiceOption
{
    public string choice;
    public string[] newStringState;
    public int nextMessageId;
}