using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [Header("�ړ����x")]
    public float moveSpeed;

    void Update()
    {

        // �X�N���v�g���A�^�b�`����Ă���Q�[���I�u�W�F�N�g�̈ʒu�����X�V���Ĉړ�������
        transform.position += new Vector3(-moveSpeed, 0, 0);

        // �X�N���v�g���A�^�b�`����Ă���Q�[���I�u�W�F�N�g���Q�[����ʂɈڂ�Ȃ��ʒu�܂ňړ�������
        if (transform.position.x <= -14.0f)
        {
            // �X�N���v�g���A�^�b�`����Ă���Q�[���I�u�W�F�N�g��j��
            Destroy(gameObject);
        }
    }
}