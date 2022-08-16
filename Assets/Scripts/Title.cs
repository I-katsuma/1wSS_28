using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] private GameObject TitlePanel;
    [SerializeField] private GameObject StoryPanel;


    // Start is called before the first frame update
    void Start()
    {
        TitlePanel.SetActive(true);
        StoryPanel.SetActive(false);
    }

    public void SwitchPanel(bool x)
    {
        StartCoroutine(DelaySwitch(0.25f, x));
    }

    IEnumerator DelaySwitch(float delay, bool x)
    {
        AudioManager.Instance.PlaySE(SESoundData.SE.ButtonPush);
        yield return new WaitForSeconds(delay);
        TitlePanel.SetActive(x);
        StoryPanel.SetActive(!x);
    }

    public void NormalSwitch(bool x)
    {
        AudioManager.Instance.PlaySE(SESoundData.SE.ButtonPush);
        TitlePanel.SetActive(x);
        StoryPanel.SetActive(!x);       
    }

    
}
