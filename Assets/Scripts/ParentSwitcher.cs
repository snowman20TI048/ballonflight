using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentSwitcher : MonoBehaviour
{
    private string player = "Player";      // Tag �ɐݒ肵�Ă��镶�������

    // ���̃X�N���v�g���A�^�b�`����Ă���Q�[���I�u�W�F�N�g�̃R���C�_�[�ƁA���̃Q�[���I�u�W�F�N�g�̃R���C�_�[���ڐG���Ă���Ԃ����ƐڐG������s�����\�b�h
    private void OnCollisionStay2D(Collision2D col)
    {

        // �ڐG���肪��������� col �ϐ��ɃR���C�_�[�̏�񂪑�������B���̃R���C�_�[�����Q�[���I�u�W�F�N�g��Tag�� player �ϐ��̒l�i"Player"�j�Ɠ���������Ȃ�
        if (col.gameObject.tag == player)
        {

            // �ڐG���Ă���Q�[���I�u�W�F�N�g(�L����)���A���̃X�N���v�g���A�^�b�`����Ă���Q�[���I�u�W�F�N�g(��)�̎q�I�u�W�F�N�g�ɂ���
            col.transform.SetParent(transform); //�e�q�֌W��ύX
        }
    }

    // ���̃X�N���v�g���A�^�b�`����Ă���Q�[���I�u�W�F�N�g�̃R���C�_�[�ƁA���̃Q�[���I�u�W�F�N�g�̃R���C�_�[�Ƃ����ꂽ�ۂɔ�����s�����\�b�h
    private void OnCollisionExit2D(Collision2D col)
    {

        // �R���C�_�[�����ꂽ���肪��������� col �ϐ��ɃR���C�_�[�̏�񂪑�������B���̃R���C�_�[�����Q�[���I�u�W�F�N�g��Tag�� player �ϐ��̒l�i"Player"�j�Ɠ���������Ȃ�
        if (col.gameObject.tag == player)
        {

            // �ڐG��Ԃł͂Ȃ��Ȃ���(���ꂽ)�Q�[���I�u�W�F�N�g(�L����)�ƁA���̃X�N���v�g���A�^�b�`����Ă���Q�[���I�u�W�F�N�g(��)�̐e�q�֌W����������
            col.transform.SetParent(null); //�e�q�֌W������
        }
    }
}