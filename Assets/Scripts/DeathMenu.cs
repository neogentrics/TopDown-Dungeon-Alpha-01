using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMusic, bgSound;


    private void Start()
    {
        deathMusic.SetActive(true);
        bgSound.SetActive(false);
    }
}
