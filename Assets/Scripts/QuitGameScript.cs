using UnityEngine;

public class QuitGameScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit requested!");
            Application.Quit();
        }
    }
}