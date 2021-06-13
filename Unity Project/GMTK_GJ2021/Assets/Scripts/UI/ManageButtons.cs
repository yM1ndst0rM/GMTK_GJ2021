using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageButtons : MonoBehaviour
{

	/* Class: ManageButtons
	 * 
	 * Purpose: Manage Buttons will handle all button management in the game.
	 * 
	 */

    // Start is called before the first frame update
    void Start()
    {
		Debug.Log("Current scene is: " + SceneManager.GetActiveScene().name);
		Debug.Log("Current scene index is: " + SceneManager.GetActiveScene().buildIndex);
	}

    public void LoadSceneByIndex()
	{
		Debug.Log("Loading new scene by index. \n" +
			"Next index is: " + SceneManager.GetActiveScene().buildIndex + 1);

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}


}
