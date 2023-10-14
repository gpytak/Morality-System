using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayer : MonoBehaviour {
    PlayerStats newPlayer = new PlayerStats();

    // Start is called before the first frame update
    // void Start()
    // {
    //     newPlayer = new CreatePlayer();
    // }

    public void CreateNewPlayer()
    {
        newPlayer.playerName = "Default";
        newPlayer.Morality = 0;
        newPlayer.Buffer = 0;
        newPlayer.Reputation = "";
    }

}
