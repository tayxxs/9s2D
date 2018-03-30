using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSenceTwo : MonoBehaviour {

    public string SenceName;
    public GameObject birthPoint;

    bool chuanSongMode;

    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            chuanSongMode = true;
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(chuanSongMode)
                StartCoroutine(LoadYourAsyncScene());
            else
            {
                other.transform.position = birthPoint.transform.position;
            }
        }

    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the Scene by build //number.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SenceName);

        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
