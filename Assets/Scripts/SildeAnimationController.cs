using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SildeAnimationController : MonoBehaviour
{
    public PlayerController playerController;
    public void DiableJumpDuringSilde()
    {
        if(playerController != null)
        {
            playerController.canJump = false;
        }
    }
}
