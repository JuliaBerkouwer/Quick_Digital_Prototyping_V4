using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingImageScript : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "PlayerShipTag")
        {
            ScoreManager.Instance.StartBlinking();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "PlayerShipTag")
        {
            ScoreManager.Instance.StopBlinking();
        }
    }
}
