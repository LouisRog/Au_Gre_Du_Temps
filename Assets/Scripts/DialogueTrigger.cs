using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;
    public string[] stringStates;

    void Start()
    {
        string resourcesPath = "Assets/Dialogs/";
        string[] jsonFiles = Directory.GetFiles(resourcesPath, "*.json");
        List<Message> all_messages = new List<Message>();

        foreach (string filePath in jsonFiles)
        {
            string json = File.ReadAllText(filePath);
            MessageList data = JsonUtility.FromJson<MessageList>(json);
            foreach (Message d in data.messages) {
                all_messages.Add(d);
            }
        }
        messages = all_messages.ToArray();
        Debug.Log("Loaded this many messages: " + messages.Length);
    }
}

public class MessageList {
    public Message[] messages;
}

[System.Serializable]
public class Message
{
    public int actorId;
    public int messageId;
    public int nextMessageId;
    public string message;
    public ChoiceOption[] choices;
    public Sprite backgroundImage;
}

[System.Serializable]
public class Actor
{
    public int actorId;
    public string name;
    public Sprite sprite;
}

[System.Serializable]
public class ChoiceOption
{
    public string choice;
    public string newStringState;
    public int nextMessage;
}