using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsReturnFlag : MonoBehaviour
{
    [SerializeField]
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (player.isReturn == false)
            {
                //player.isReturn = true;
                player.SwitchReturnAnim(true);

            }
            else if (player.isReturn == true)
            {
                //player.isReturn = false;
                player.SwitchReturnAnim(false);
            }
        }
    }
}
