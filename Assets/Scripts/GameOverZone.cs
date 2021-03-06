using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverZone : MonoBehaviour
{

    ////* ここから追加 *////

    [SerializeField]
    private AudioManager audioManager;

    ////* ここまで *////


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerController>().GameOver();

            Debug.Log("Game Over");


            ////* ここから追加 *////

            StartCoroutine(audioManager.PlayBGM(3));

            ////* ここまで *////

        }
    }
}
