using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	const int ROBOT_SCENE = 1;



	public void LoadScene(int SceneIndex){
		SceneManager.LoadScene (SceneIndex);
	}
}
