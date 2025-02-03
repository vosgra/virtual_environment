using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButtonScript : MonoBehaviour
{
    // This method will be called when the exit button is clicked
    public void ExitGame()
    {
        // Logs a message to the console (useful for debugging in the editor)
        Debug.Log("Exit button clicked. Application will close.");

        // Closes the application
        Application.Quit();
    }
}
