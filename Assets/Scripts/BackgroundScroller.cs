using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Header("�w�i�摜�̃X�N���[�����x = �����X�N���[���̑��x")]
    public float scrollSpeed = 0.01f;

    [Header("�摜�̃X�N���[���I���n�_")]
    public float stopPosition = -16f;

    [Header("�摜�̍ăX�^�[�g�n�_")]
    public float restartPosition = 5.8f;

    void Update()
    {

        // ��ʂ̍������ɂ��̃Q�[���I�u�W�F�N�g(�w�i)�̈ʒu���ړ�����
        transform.Translate(-scrollSpeed, 0, 0);

        // ���̃Q�[���I�u�W�F�N�g�̈ʒu��stopPosition�ɓ��B������
        if (transform.position.x < stopPosition)
        {

            // �Q�[���I�u�W�F�N�g�̈ʒu���ăX�^�[�g�n�_�ֈړ�����
            transform.position = new Vector2(restartPosition, 0);
        }
    }
}