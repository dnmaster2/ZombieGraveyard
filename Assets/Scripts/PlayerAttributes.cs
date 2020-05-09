using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttributes : MonoBehaviour
{
    public int playerHealth = 100;
    public GameObject fadeout;
    public AnimationClip clip;

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        fadeout.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        yield return new WaitForSeconds(clip.length);
        SceneManager.LoadSceneAsync(3);
    }
}
