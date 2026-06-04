using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocarCena : MonoBehaviour
{
    public void NovoJogo()
    {
        SceneManager.LoadScene("NovoJogo");
    }
}