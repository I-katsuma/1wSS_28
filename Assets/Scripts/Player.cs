using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float myMoveSpeed = 3f;

    public Transform CatchItemPoint;

    [SerializeField]
    private SpacemanManager spacemanManager;

    [SerializeField]
    private SpriteRenderer spacemanSprite;

    [SerializeField]
    private Collider2D SpacemanCollider;

    [SerializeField]
    private Animator animator;
    private Vector3 move = Vector3.zero;

    public bool isReturn = false; // 折り返しフラグ

    [SerializeField]
    private bool isCatchItem = false; // アイテム所持判定

    [SerializeField] GameObject UpArrow;
    [SerializeField] GameObject DownArrow;

    // 移動制限用
    private Vector2 playerPos;
    private readonly float PosXClamp = 2.0f;
    private readonly float PosYUpClamp = 2.5f;
    private readonly float PosYDownClamp = -25.5f;

    // 行動管理
    public enum PLAYER_MODE
    {
        STOP,
        MOVE,
        DANCE,
    }

    public PLAYER_MODE mode = PLAYER_MODE.STOP;

    void Start()
    {
        //UpArrow.SetActive(false);
        //DownArrow.SetActive(false);

        isCatchItem = false;
        ArrowSwitch(false);
        animator.SetFloat("y", -1f);
    }


    void ArrowSwitch(bool x) // 矢印の向き
    {
        UpArrow.SetActive(x);
        DownArrow.SetActive(!x);       
    }

    public bool ItemCatchCheck() // あくまでチェック用
    {
        if (isCatchItem == true)
        {
            return true;
        }
        return false;
    }

    public void SetItemCatchFlag(bool flag) // オンオフ用
    {
        if (flag)
        {
            isCatchItem = true;
        }
        else
        {
            isCatchItem = false;
            spacemanManager.RemoveItem();
        }
    }

    public void SwitchReturnAnim(bool flag) // Playerスプライトの向きを変える
    {
        if (flag) // isRturnがtrueなら↓から↑へ
        {
            isReturn = true;
            ArrowSwitch(true);
            animator.SetFloat("y", 1);
        }
        else // ↑から↓へ
        {
            isReturn = false;
            ArrowSwitch(false);
            animator.SetFloat("y", -1);
        }
    }

    public void ItemCatchAction(bool flag)
    {
        // アイテムを取得したら折り返しアクション(↓方向進行時のみ)
        if (flag)
        {
            isCatchItem = true;
            spacemanManager.SetItem();
            if (isReturn == false)
            {
                SwitchReturnAnim(true);
            }
        }
    }

    void Update()
    {
        if (mode == PLAYER_MODE.MOVE)
        {
            if (isReturn)
            {
                this.transform.Translate(Vector3.up * Time.deltaTime * myMoveSpeed);
            }
            else
            {
                this.transform.Translate(Vector3.down * Time.deltaTime * myMoveSpeed);
            }

            this.MovingRestrictions();

            if (this.move != Vector3.zero)
            {
                this.transform.Translate(this.move * Time.deltaTime * myMoveSpeed);
            }
        }
        else if(mode == PLAYER_MODE.STOP)
        {
            return;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        this.move = context.ReadValue<Vector2>();
        var normalized = new Vector3(
            Mathf.Round(move.normalized.x),
            Mathf.Round(move.normalized.y),
            0
        );

        if (normalized != Vector3.zero) // アニメ設定
        {
            this.animator.SetFloat("x", normalized.x);
            this.animator.SetFloat("y", normalized.y);
        }
    }

    /// <summary>
    /// 移動制限
    /// </summary>
    private void MovingRestrictions()
    {
        this.playerPos = transform.position;

        this.playerPos.x = Mathf.Clamp(this.playerPos.x, -this.PosXClamp, this.PosXClamp);
        this.playerPos.y = Mathf.Clamp(this.playerPos.y, this.PosYDownClamp, this.PosYUpClamp);

        transform.position = new Vector2(this.playerPos.x, this.playerPos.y);
    }
}
