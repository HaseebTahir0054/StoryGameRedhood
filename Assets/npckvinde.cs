using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class npcmand : MonoBehaviour
{
    public NPCConversation myConversation;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ConversationManager.Instance.StartConversation(myConversation);
        }
    }
}
