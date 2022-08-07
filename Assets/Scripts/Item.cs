using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject itemShadow;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        itemShadow.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("isTrigger");
            if(player.isCatchItem == false)
            {
                Transform item = this.gameObject.transform;
                item.SetParent(player.CatchItemPoint.transform);
                item.transform.position = new Vector3(
                    player.CatchItemPoint.position.x,
                     player.CatchItemPoint.position.y, 0);
                itemShadow.SetActive(false);
                player.ItemCatchAction(true);
            }
            else
            {
                return;
            }
        }
    }
}
