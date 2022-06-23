using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VerticalFloatingObject : MonoBehaviour
{
    public float moveTime;
    public float moveRange;


    void Start()
    {
        // DOTween による命令を実行し、SetLink メソッドを利用してゲームオブジェクトの破棄に Tween の終了を紐付けする
        transform.DOMoveY(transform.position.y - moveRange, moveTime)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(gameObject);
    }
}