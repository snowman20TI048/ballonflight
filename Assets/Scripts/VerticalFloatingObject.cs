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
        // DOTween �ɂ�閽�߂����s���ASetLink ���\�b�h�𗘗p���ăQ�[���I�u�W�F�N�g�̔j���� Tween �̏I����R�t������
        transform.DOMoveY(transform.position.y - moveRange, moveTime)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(gameObject);
    }
}