using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ballon : MonoBehaviour
{
    private PlayerController playerController;

    private Tweener tweener;

    /// <summary>
    /// �o���[���̏����ݒ�
    /// </summary>
    public void SetUpBallon(PlayerController playerController)
    {
        this.playerController = playerController;

        // �{����Scale��ێ�
        Vector3 scale = transform.localScale;

        // ���݂�Scale��0�ɂ��ĉ�ʂ���ꎞ�I�ɔ�\���ɂ���
        transform.localScale = Vector3.zero;

        // ���񂾂�o���[�����c��ރA�j�����o
        transform.DOScale(scale, 2.0f)
            .SetEase(Ease.InBounce);

        // ���E�ɂӂ�ӂ킳����
        tweener = transform.DOLocalMoveX(0.02f, 0.2f)
            .SetEase(Ease.Flash)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {

            // ���E�ɂӂ�ӂ킳���郋�[�v�A�j����j������
            tweener.Kill();

            // PlayerController��DestroyBallon���\�b�h���Ăяo���A�o���[���̔j�󏈗����s��
            playerController.DestroyBallon();
        }
    }
}