using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBunny : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("うさぎがPlayerタグと接触");
        }

        else if(other.gameObject.tag == "Spaceman")
        {
            Debug.Log("うさぎがSpacemanと接触");
        }    
    }

}
