using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    
    public float transitionTime = 1f;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1));
    } 

    IEnumerator LoadNextLevel(int levelIndex)
    {
        transition.SetTrigger("start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
