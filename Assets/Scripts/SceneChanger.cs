using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    [SerializeField] Fade fade;

    private void Start() {
        fade.FadeOut(1f);
    }

    public void OnNexeScene(int nextScene)
    {
        AudioManager.Instance.PlaySE(SESoundData.SE.ButtonPush);
        fade.FadeIn(1f,
        () => SceneManager.LoadScene(nextScene));
        GameManager.Instance.initialize();
        GameManager.Instance.SetPlayBTN(nextScene);
    }

}
