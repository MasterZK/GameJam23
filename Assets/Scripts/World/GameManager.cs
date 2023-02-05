using UnityEngine;

public class GameManager : MonoBehaviour
{

    private TimeCycle time;

    private void Start()
    {
        time = GameObject.FindObjectOfType<TimeCycle>();
    }

    private void StartGame()
    {
        time.startTime = true; 

        //start spawner for enemies. 
        //allow control of players movement and actions
    }

    public void PauseGame(bool state)
    {
        //pause/unpause spawners 
        //deactivate player movement 
    }

}

