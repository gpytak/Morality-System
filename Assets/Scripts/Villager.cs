using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{
    public ActorSO actorSO; // Array which holds the actors
    public int behavior = 0; // Randomly generated behavior of the villager
    public bool taskStarted = false; // If the villager's task has been started
    public bool taskFinished = false; // If the villager's task has been finished
    public bool taskComplete = false; // If the villager's task has been completed
}
