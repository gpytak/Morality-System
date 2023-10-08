using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private string _playerName = "";
    private int _morality = 0;
    private string _reputation = "";

    public string playerName {
        get{ return _playerName; }
        set{ _playerName = value; }
    }

    public int Morality {
        get{ return _morality; }
        set{ _morality = value; }
    }

    public string Reputation {
        get{ return _reputation; }
        set{ _reputation = value; }
    }

    public struct normalActions {
        public int moralityValue { get; set; } // 1 = Good, 0 = Neutral, -1 = Bad
        public float actionWeight { get; set; } // Measured based on the degree of the importance and permanence
    }

    public struct keyActions {
        public int moralityValue { get; set; } // 1 = Good, 0 = Neutral, -1 = Bad
        public float actionWeight { get; set; } // Measured based on the degree of the importance and permanence
        public string keyEvent { get; set; } // A summary of the key action with event details and are used alongside reputations
    }
}
