using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float myMoveSpeed = 3f;

    public Transform CatchItemPoint;
    [SerializeField] private SpriteRenderer spacemanSprite;
    [SerializeField] private Collider2D SpacemanCollider;
    [SerializeField] private Animator animator;
    private Vector3 move = Vector3.zero;

    public bool isReturn = false; // 折り返しフラグ

    public bool isCatchItem = false; // アイテム所持判定

    // 移動制限用
    private Vector2 playerPos;
    private readonly float PosXClamp = 2.0f;
    private readonly float PosYUpClamp = 2.5f;
    private readonly float PosYDownClamp = -25.5f;

    void Start()
    {
        isCatchItem = false;
        animator.SetFloat("y", -1f);
    }

    public bool ItemCatchCheck()
    {
        if(isCatchItem == true)
        {
            return true;
        }
        return false;
    }

    public void SwitchReturnAnim(bool flag)
    {
        if(flag) // isRturnがtrueなら↓から↑へ
        {
            isReturn = true;
            animator.SetFloat("y", 1);
        }
        else // ↑から↓へ
        {
            isReturn = false;
            animator.SetFloat("y", -1);
        }
    }

    public void ItemCatchAction(bool flag)
    {
        if(flag)
        {
            isCatchItem = true;
            if(isReturn == false)
            {
                SwitchReturnAnim(true);
            }
        }
    }

    void Update()
    {
        if(isReturn)
        {
            this.transform.Translate(Vector3.up * Time.deltaTime * myMoveSpeed);
        }
        else
        {
            this.transform.Translate(Vector3.down * Time.deltaTime * myMoveSpeed);
        }
        
        this.MovingRestrictions();
        
        if(this.move != Vector3.zero)
        {
            this.transform.Translate(this.move * Time.deltaTime * myMoveSpeed);

            /*
            if(this.move.x > 0) {
                this.transform.position = new Vector3(Time.deltaTime * myMoveSpeed, 
                this.transform.position.y,
                0);
            }
            else if(this.move.x < 0) {
                this.transform.position = new Vector3(Time.deltaTime * -myMoveSpeed, 
                this.transform.position.y,
                0);
            }
            */
        }
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        this.move = context.ReadValue<Vector2>();
        var normalized = new Vector3(Mathf.Round(move.normalized.x), Mathf.Round(move.normalized.y), 0);

        if(normalized != Vector3.zero) // アニメ設定
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
