using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{

    [SerializeField] GameObject thePlayer;
    //[SerializeField] GameObject playerAnim; // todo readd this?
    [SerializeField] AudioSource collisionFX;
    [SerializeField] GameObject mainCam;
    [SerializeField] GameObject fadeOut;
    [SerializeField] SceneLoader loader;

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(CollisionEnd());
    }

    IEnumerator CollisionEnd()
    {
        collisionFX.Play();
        thePlayer.GetComponent<PlayerMovement>().enabled = false;
        SaveLoadManager.Instance.SaveGame();
        //playerAnim.GetComponent<Animator>().Play("Stumble Backwards"); // todo readd this
        mainCam.GetComponent<Animator>().Play("CollisionCam");
        yield return new WaitForSeconds(1);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(3);
        loader.loadMainMenu();
    }
}
