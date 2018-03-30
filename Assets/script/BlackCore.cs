using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackCore : MonoBehaviour {

    public float F;
    public bool RestCore;
    public string SenceName;
    Rigidbody2D rb;
    CircleCollider2D[] C2D;
    bool more;
    public GameObject muBiao;
    GameObject Player;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        C2D = GetComponents<CircleCollider2D>();
        Debug.Log(C2D);
        Player = GameObject.Find("Player2D");
        if(muBiao==null&&!RestCore)
        {
            muBiao = Player;
        }
        if (muBiao == null)
            more = true;
        else
            more = false;
            
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

	// Update is called once per frame
	void Update () {
        if(more)
            C2D[0].radius++;
        if(muBiao!=null)
        {
            rb.AddForce((muBiao.transform.position - this.transform.position).normalized * F);
            //rb.MovePosition((muBiao.transform.position - this.transform.position).normalized*F);
        }

        if(Input.GetKeyDown(KeyCode.Escape)&&RestCore)
        {
            Application.LoadLevel(Application.loadedLevelName);
        }
        //if(C2D[0]==null&&C2D[1]==null)
        //{
        //    if(RestCore)
        //    {

        //        Debug.Log(1);
        //        Application.LoadLevel(Application.loadedLevelName);

        //    }

        //    Destroy(this.gameObject);
        //}
            
        
            
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            more = false;
            muBiao = collision.transform.gameObject;
            if (C2D[0] == null)
                Destroy(C2D[1]);
            else
                Destroy(C2D[0]);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.layer == LayerMask.NameToLayer("Monster")||collision.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (RestCore)
            {
                Application.LoadLevel(Application.loadedLevelName);

            }

            Destroy(this.gameObject);
        }
    }
}
