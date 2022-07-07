using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject aerialFloorPrefab;     // �v���t�@�u�ɂ��� AerialFloor_Mid �Q�[���I�u�W�F�N�g���C���X�y�N�^�[����A�T�C������

    [SerializeField]
    private Transform generateTran;           // �v���t�@�u�̃N���[���𐶐�����ʒu�̐ݒ�

    [Header("�����܂ł̑ҋ@����")]
    public float waitTime;                    // �P�񐶐�����܂ł̑ҋ@���ԁB�ǂ̈ʂ̊Ԋu�Ŏ����������s�����ݒ�

    private float timer;                      // �ҋ@���Ԃ̌v���p

    private GameDirector gameDirector;


    ////* ��������ǉ� *////


    private bool isActivate;                  // �����̏�Ԃ�ݒ肵�A�������s�����ǂ����̔���ɗ��p����Btrue�Ȃ� �������Afalse �Ȃ琶�����Ȃ�


    ////* �����܂� *////


    void Update()
    {


        ////* ��������ǉ� *////

        // ��~���͐������s��Ȃ�
        if (isActivate == false)
        {
            return;
        }

        ////* �����܂� *////


        // ���Ԃ��v������
        timer += Time.deltaTime;

        // �v�����Ă��鎞�Ԃ� waitTime �̒l�Ɠ������A��������
        if (timer >= waitTime)
        {

            // ����̌v���p�ɁAtimer �� 0 �ɂ���
            timer = 0;

            // �N���[�������p�̃��\�b�h���Ăяo��
            GenerateFloor();
        }
    }

    /// <summary>
    /// �v���t�@�u�����ɃN���[���̃Q�[���I�u�W�F�N�g�𐶐�
    /// </summary>
    private void GenerateFloor()
    {

        // �󒆏��̃v���t�@�u�����ɃN���[���̃Q�[���I�u�W�F�N�g�𐶐�
        GameObject obj = Instantiate(aerialFloorPrefab, generateTran);

        // �����_���Ȓl���擾
        float randomPosY = Random.Range(-4.0f, 4.0f);

        // �������ꂽ�Q�[���I�u�W�F�N�g��Y���Ƀ����_���Ȓl�����Z���āA��������邽�тɍ����̈ʒu��ύX����
        obj.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + randomPosY);

        // ���������J�E���g�A�b�v
        gameDirector.GenerateCount++;
    }

    /// <summary>
    /// �W�F�l���[�^�̏���
    /// </summary>
    /// <param name="gameDirector"></param>
    public void SetUpGenerator(GameDirector gameDirector)
    {
        this.gameDirector = gameDirector;
    }


    ////* �V�������\�b�h����������ǉ� *////


    /// <summary>
    /// ������Ԃ̃I��/�I�t��؂�ւ�
    /// </summary>
    /// <param name="isSwitch"></param>
    public void SwitchActivation(bool isSwitch)
    {
        isActivate = isSwitch;
    }


    ////* �����܂� *////

}