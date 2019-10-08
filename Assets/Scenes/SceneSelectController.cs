using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelectController : MonoBehaviour
{
    public void selectScene()
    {
        switch(this.gameObject.name)
        {
            case "SceneButton001":
                SceneManager.LoadScene("001A-House");
                break;
            case "SceneButton002":
                SceneManager.LoadScene("002A-DreamWorld");
                break;
            case "SceneButton003":
                SceneManager.LoadScene("003A-DreamWorld");
                break;
            case "SceneButton004":
                SceneManager.LoadScene("004A-DreamWorld");
                break;
        }
    }
}
