using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;
    public Choice[] choices;
    public string[] stringState;

}

[System.Serializable]
public class Message
{
    public int actorId;
    public int messageId;
    public bool isChoiceNode;
    public int nextMessageId;
    public string message;
}

[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}

[System.Serializable]
public class Choice
{
    public int messageNode;
    public ChoiceOption[] choices;
}

[System.Serializable]
public class ChoiceOption
{
    public string choices;
    public string newStringState;
    public int nextMessage;
}