using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public GameObject camera;
    public Collider2D trigger;

    void Awake()
    {
        camera.GetComponent<CameraFollower>().enabled = false;
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);
        Player.Instance.inputController.detectInput = false;
        Player.Instance.movementController.StopMoving();
        Invoke("BackToPlayer", 2.0f);
    }
    
    void BackToPlayer()
    {
        camera.GetComponent<CameraFollower>().enabled = true;
        Player.Instance.inputController.detectInput = true;
        trigger.enabled = true;
    }
}
