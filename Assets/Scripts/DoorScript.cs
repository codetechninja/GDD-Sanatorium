using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    //private Animator _animator;
    //private GameObject player;
    //private bool doorOpen;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //_animator = GetComponent<Animator>();
        //player = GameObject.Find("Player");
        //doorOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * void OnTriggerEnter(Collider other)
    {
        
        PlayerInteractionsScript playerScript = player.GetComponent<PlayerInteractionsScript>();

        if(gameObject.name.Contains("InteractiveDoor")){
            if (other.tag == "Player") {
                doorOpen = true;
                Doors("Open");
            }
        }

        for (int i = 0; i < playerScript.nbCards; i++)
        {
            if ((gameObject.name.Contains(playerScript.cardsColor[i] + "Door") && playerScript.holdCards[i]))
            {
                if (other.tag == "Player")
                {
                    doorOpen = true;
                    Doors("Open");
                }
            }

            if (playerScript.cardsColor[i] == "Blue" && gameObject.name.Contains("BlueDoor") && playerScript.holdCards[i])
            {
                // Use a coroutine to load the Scene in the background
                StartCoroutine(playerScript.LoadYourAsyncScene(1));
            }
        }

        //Physics.IgnoreCollision(GetComponent<Collider>(), otherObj.gameObject.GetComponent<Collider>());
        //The blue barrier let go to the next scene

    }

    
    void OnTriggerExit(Collider col)
    {
        if (doorOpen)
        {
            doorOpen = false;
            Doors("Close");
        }

    }

    void Doors(string direction)
    {
        _animator.SetTrigger(direction);
    }
    
   
    IEnumerator LoadYourAsyncScene(int i)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentSceneIndex+i);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    */
}
