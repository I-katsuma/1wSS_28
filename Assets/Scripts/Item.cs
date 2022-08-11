using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Item : MonoBehaviour
{
    private Vector3 itemPos;

    [SerializeField] private Player player;
    [SerializeField] private GameObject itemShadow;
    [SerializeField] private Transform ItemsGameObject;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ItemsGameObject = GameObject.FindGameObjectWithTag("ItemsBox").GetComponent<Transform>();
        itemShadow.SetActive(true);
    }

    public void DropItem()
    {
        this.transform.SetParent(ItemsGameObject);
        player.SetItemCatchFlag(false);
        AudioManager.Instance.PlaySE(SESoundData.SE.Damage);
        StartCoroutine("MoveItem");
    }


    IEnumerator MoveItem()
    {
        int num = 0; // while文のカウント用
        float moveNum = 0.30f;
        float playerPosX = player.transform.position.x;

        if(playerPosX > 0) { moveNum = 0.30f; } // →移動
        else if(playerPosX < 0) { moveNum = -0.30f; } // ←移動
        while(num < 5)
        {
            itemPos = this.transform.position;
            if(itemPos.x > 2.0f || itemPos.x < -2.0f) // 範囲外対策？（未検査）
            {
                num += 5;
            }
            this.transform.Translate(moveNum, 0, 0);
            num++;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.tag == "Player")
        {
            if(player.ItemCatchCheck() == false)
            {
                Transform item = this.gameObject.transform;
                item.SetParent(player.CatchItemPoint.transform);
                item.transform.position = new Vector3(
                    player.CatchItemPoint.position.x,
                     player.CatchItemPoint.position.y, 0);
                itemShadow.SetActive(false);
                player.ItemCatchAction(true);
                AudioManager.Instance.PlaySE(SESoundData.SE.ItemGet);
            }
            else
            {
                return;
            }
        }
    }
    void Update()
    {
        /*
        var current = Keyboard.current;
                // キーボード接続チェック
        if (current == null)
        {
            // キーボードが接続されていないと
            // Keyboard.currentがnullになる
            return;
        }

        // Aキーの入力状態取得
        var fKey = current.fKey;

        // Aキーが押された瞬間かどうか
        if (fKey.wasPressedThisFrame)
        {
            DropItem();
        }
        */
    }
}
