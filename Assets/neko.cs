using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class neko : MonoBehaviour
{

    [SerializeField] Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.state == GameManager.SCENE_STATE.STAGE2)
        {
            anim.SetBool("Sit", true);
        }
    }


}
