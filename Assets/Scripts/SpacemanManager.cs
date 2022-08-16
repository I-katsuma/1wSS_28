using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpacemanManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private Item catchItem;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Spacemanとうさぎが接触したで");
            if (player.ItemCatchCheck() == true)
            {
                Debug.Log("アイテム持ってるやん邪魔したろ");
                if (catchItem != null)
                {
                    Debug.Log("アイテム飛ばしたったで");
                    catchItem.DropItem();
                }
            }
        }
    }

    public void JumpSE()
    {
        if (player.mode == Player.PLAYER_MODE.STOP)
        {
            return;
        }
        else
        {
            AudioManager.Instance.PlaySE(SESoundData.SE.Jump);
        }
    }

    public void SetItem()
    {
        catchItem = player.CatchItemPoint.transform.GetChild(0).gameObject.GetComponent<Item>();
        if (catchItem != null)
        {
            Debug.Log("セット完了");
        }
    }

    public void RemoveItem()
    {
        catchItem = null;
        if (catchItem == null)
        {
            Debug.Log("リムーブ完了");
        }
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        if(player.ItemCatchCheck())
        {
            SetItem();
        }
        */
    }
}
