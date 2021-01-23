using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    GameObject player;
    PlayerControl playerControl;

    public void Initialize()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerControl = player.GetComponent<PlayerControl>();
    }
   public void JumpClick()
   {
       Debug.Log("Jump");
       playerControl.inputJump = true;
   }
}
