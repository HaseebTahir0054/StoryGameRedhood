using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class kvinde : MonoBehaviour
{
    public NPCConversation myConversation;
    //bool conversationStarted;

   // public Transform player;
   // public float distanceToStartConversation = 5;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // && !conversationStarted && Vector3.Distance(transform.position, player.position) < distanceToStartConversation)
        {
            //conversationStarted = true;
            ConversationManager.Instance.StartConversation(myConversation);
        }
    }
}
