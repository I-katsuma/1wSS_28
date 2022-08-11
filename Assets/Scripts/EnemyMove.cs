using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Vector3 pos;
    public float speed = 1.5f;
    public int allowNum = 1;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public enum TYPE
    {
        TYPE1,
        TYPE2,
        TYPE3
    }

    public TYPE rabbitType = TYPE.TYPE1;

    private void LoopMove()
    {
        pos = transform.position;

        transform.Translate(transform.right * Time.deltaTime * speed * allowNum);

        if (pos.x < -2.5f)
        {
            allowNum = 1;
            spriteRenderer.flipX = false;
        }
        if (pos.x > 2.5f)
        {
            allowNum = -1;
            spriteRenderer.flipX = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (rabbitType == TYPE.TYPE1)
        {
            anim.SetBool("run", true);
        }
        else if(rabbitType == TYPE.TYPE2)
        {
            anim.SetBool("jump", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        LoopMove();
    }
}
