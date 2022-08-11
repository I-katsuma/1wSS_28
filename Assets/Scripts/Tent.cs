using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tent : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TimeAndScore itemManager;

    //[SerializeField] GameManager gameManager;

    private void Awake() 
    {
        //gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Tentにたっち");
            if(player.ItemCatchCheck() == true)
            {
                Debug.Log("アイテム持ってる");
                if(player.CatchItemPoint.transform.childCount > 0)
                {
                    Debug.Log("持ってるの消したよ");
                    GameManager.Instance.ItemCountMethod(); // カウント増やす
                    itemManager.UpdateItemCount(); // カウント表示更新
                    Destroy(player.CatchItemPoint.GetChild(0).gameObject);
                    AudioManager.Instance.PlaySE(SESoundData.SE.Tent);
                    player.SetItemCatchFlag(false);
                }
            }else
            {
                Debug.Log("なんももってないやん");
            }

        }    
    }
}
