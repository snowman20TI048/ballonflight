using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoalChecker : MonoBehaviour
{
    public float moveSpeed = 0.01f;      // �ړ����x

    private float stopPos = 6.5f;        // ��~�n�_�B��ʂ̉E�[�ŃX�g�b�v������

    private bool isGoal;                 // �S�[���̏d������h�~�p�B��x�S�[�����肵���� true �ɂ��āA�S�[���̔���͂P�񂾂������s��Ȃ��悤�ɂ���


    ////* ��������ǉ� *////


    private GameDirector gameDirector;


    ////* �����܂� *////


    void Update()
    {

        // ��~�n�_�ɓ��B����܂ňړ�����
        if (transform.position.x > stopPos)
        {
            transform.position += new Vector3(-moveSpeed, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        // �ڐG����(�S�[������)�ۂɂP�񂾂����肷��
        if (col.gameObject.tag == "Player" && isGoal == false)
        {

            // �Q��ڈȍ~�̓S�[��������s��Ȃ��悤�ɂ��邽�߂ɁAtrue �ɕύX����
            isGoal = true;

            Debug.Log("�Q�[���N���A");

            // PlayerController�̏����擾
            PlayerController playerController = col.gameObject.GetComponent<PlayerController>();

            // PlayerController�̎��AUIManager�̕ϐ��𗘗p���āAGenerateResultPopUp���\�b�h���Ăяo���B�����ɂ�PlayerController��coinCount��n��
            playerController.uiManager.GenerateResultPopUp(playerController.coinPoint);



            ////* ��������ǉ� *////


            // �S�[������
            gameDirector.GoalClear();


            ////* �����܂� *////

        }
    }



    ////* �������烁�\�b�h���P�ǉ� *////

    /// <summary>
    /// �S�[���n�_�̏����ݒ�
    /// </summary>
    public void SetUpGoalHouse(GameDirector gameDirector)
    {

        this.gameDirector = gameDirector;

        // TODO ���ɏ����ݒ肪�K�v�ȏꍇ�ɂ͂����ɒǉ�����
    }

    ////* �����܂� *////

}