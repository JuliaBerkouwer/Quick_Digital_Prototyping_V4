using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCollision : MonoBehaviour
{


    IEnumerator WaitForNextLevel()
    {
        Debug.Log("itsinn");
        ScoreManager.Instance.WinUI();
        yield return new WaitForSeconds(5);
        FindObjectOfType<LoadNextLevel>().LoadLevel();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "PlayerShipTag")
        {
            StartCoroutine("WaitForNextLevel");
            Debug.Log("You win!");
        }

    }
}
