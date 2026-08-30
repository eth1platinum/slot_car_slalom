using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{

    [SerializeField] GameObject thePlayer;
    [SerializeField] AudioSource collisionFX;
    [SerializeField] GameObject mainCam;
    [SerializeField] GameObject fadeOut;
    [SerializeField] SceneLoadWrapper wrapper;

    public RunTimer runTimer;

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(CollisionEnd());
    }

    IEnumerator CollisionEnd()
    {
        collisionFX.Play();
        thePlayer.GetComponent<PlayerMovement>().enabled = false;
        runTimer.StopRun();
        SaveLoadManager.Instance.SaveGame();
        mainCam.GetComponent<Animator>().Play("CollisionCam");
        yield return new WaitForSeconds(1);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(3);
        wrapper.loadMainMenu();
    }
}
