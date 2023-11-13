using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private Message[] messages;
    public string[] stringStates;

    void Start()
    {
        messages = new Message[1000]; // Make sure its big enough for our proto
        string resourcesPath = "Assets/StreamingAssets/";
        string[] jsonFiles = Directory.GetFiles(resourcesPath, "*.json");
        List<Message> allMessages = new List<Message>();

        foreach (string filePath in jsonFiles)
        {
            string json = File.ReadAllText(filePath);
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

    public void StartDialogue()
    {
        FindAnyObjectByType<DialogueManager>().OpenDialogue(messages);
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
    public string message;
    public ChoiceOption[] choices;
    public Sprite backgroundImage;
}

[System.Serializable]
public class ChoiceOption
{
    public string choice;
    public string[] newStringState;
    public int nextMessage;
}