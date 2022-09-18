using UnityEngine;
using UnityEngine.SceneManagement;


public class Portal : Collidable
{
    
    public string[] sceneNames;
    [SerializeField] AudioSource portalSoundEffect;

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            // Teleport Player
            GameManager.instance.SaveState();
            portalSoundEffect.Play();
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            SceneManager.LoadScene(sceneName);
        }
    }
}
