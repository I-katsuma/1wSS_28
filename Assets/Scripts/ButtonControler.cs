using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonControler : MonoBehaviour
{
    void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void Start() {
        Init();
    }

    private void OnEnable() {
        Init();
    }

    void OnClick()
    {
        // 1,1f はスケール 0.5fは時間
        transform.DOScale(1.1f, 0.5f).SetEase(Ease.OutElastic).SetLoops(1, LoopType.Restart);
        //Invoke("Init", 0.5f);
    }

    void Init()
    {
        this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
