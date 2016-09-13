using UnityEngine;
using System.Collections;

public class respawner : MonoBehaviour {

    public playerDamageRespawn playerManager;

    void OnDestroy()
    {
        playerManager.spawnerDied = true;
    }

}
