using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public string teleportFrom;
    public string teleportTo;

    public void TeleportToScene()
    {
        TeleportManager.Instance.Teleport(teleportFrom, teleportTo);
    }
}
