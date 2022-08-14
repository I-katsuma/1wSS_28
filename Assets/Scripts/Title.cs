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
        SwitchPanel(true);
    }

    public void SwitchPanel(bool x)
    {
        TitlePanel.SetActive(x);
        StoryPanel.SetActive(!x);
    }
}
