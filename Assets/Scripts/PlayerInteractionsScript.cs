using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteractionsScript : MonoBehaviour
{
    public string[] cardsColor = { "Red", "Green", "Yellow", "Purple", "Blue" };
    public bool[] holdCards; 
    public int life;

    public float invincible;

    public int nbCards;

    //private Animator _animator;


    

    // Start is called before the first frame update
    void Start()
    {
        life = 5;
        nbCards = cardsColor.Length;
        holdCards = new bool[nbCards];
    }

    // Update is called once per frame
    void Update()
    {
        if(life <= 0)
        {
            //Application.LoadLevel(Application.loadedLevel);
            StartCoroutine(LoadYourAsyncScene(0));
        }

        if(invincible >= 0)
        {
            invincible -= Time.deltaTime;
        }
        
    }

    void OnCollisionEnter(Collision otherObj)
    {
        //On collision with a KeyCard, we destroy the KeyCard and we set the matching holdCards at true
        if (otherObj.gameObject.tag == "KeyCard")
        { 
            for (int i = 0; i< nbCards; i++)
            {
                if (otherObj.gameObject.name.Contains(cardsColor[i] + "KeyCard"))
                //if (otherObj.gameObject.name == cardsColor[i] + "KeyCard")
                {
                    this.holdCards[i] = true;
                    Destroy(otherObj.gameObject);
                }
            }
        }

        //If the matching holdCards is true the barrier isn't solid anymore at contact

        if (otherObj.gameObject.tag == "Door")
        {
            if (otherObj.gameObject.name.Contains("InteractiveDoor"))
            {
                Destroy(otherObj.gameObject);
                //Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), otherObj.gameObject.GetComponent<Collider>());
            }

            for (int i = 0; i < nbCards; i++)
            {
                if ((otherObj.gameObject.name.Contains(cardsColor[i] + "Door") && holdCards[i]))
                {
                    Destroy(otherObj.gameObject);
                    //Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), otherObj.gameObject.GetComponent<Collider>());
                }

                if (cardsColor[i] == "Blue" && otherObj.gameObject.name.Contains("BlueDoor") && holdCards[i])
                {
                    // Use a coroutine to load the Scene in the background
                    StartCoroutine(LoadYourAsyncScene(1));
                }
            }
        }
    }

    public IEnumerator LoadYourAsyncScene(int i)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.
        if (i <= 2)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentSceneIndex + i);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }

}

