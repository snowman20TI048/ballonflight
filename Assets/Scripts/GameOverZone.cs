using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverZone : MonoBehaviour
{

    ////* ‚±‚±‚©‚ç’Ç‰Á *////

    [SerializeField]
    private AudioManager audioManager;

    ////* ‚±‚±‚Ü‚Å *////


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerController>().GameOver();

            Debug.Log("Game Over");


            ////* ‚±‚±‚©‚ç’Ç‰Á *////

            StartCoroutine(audioManager.PlayBGM(3));

            ////* ‚±‚±‚Ü‚Å *////

        }
    }
}
