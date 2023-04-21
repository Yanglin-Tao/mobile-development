using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public PlayerMap playerMap;

    public void OnHelpButtonClick() {
        playerMap.StopFollowingCursor();
    }
}
