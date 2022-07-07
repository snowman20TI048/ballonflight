using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    // �S�[��
    [SerializeField]
    private GoalChecker goalHousePrefab;

    // Player
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private FloorGenerator[] floorGenerators;

    [SerializeField]
    private RandomObjectGenerator[] randomObjectGenerators;

    [SerializeField]
    private AudioManager audioManager;

    private bool isSetUp;                           // �Q�[���̏�������p�Btrue �ɂȂ�ƃQ�[���J�n

    private bool isGameUp;                          // �Q�[���I������p�Btrue �ɂȂ�ƃQ�[���I��

    private int generateCount;                      // �󒆏��̐�����

    // generateCount �ϐ��̃v���p�e�B
    public int GenerateCount
    {
        set
        {
            generateCount = value;

            Debug.Log("������ / �N���A�ڕW�� : " + generateCount + " / " + clearCount);

            if (generateCount >= clearCount)
            {
                // �S�[���n�_�𐶐�
                GenerateGoal();

                // �Q�[���I��
                GameUp();
            }
        }
        get
        {
            return generateCount;
        }
    }

    public int clearCount;�@�@�@�@�@�@�@�@�@�@�@ �@// �S�[���n�_�𐶐�����܂łɕK�v�ȋ󒆏��̐�����


    void Start()
    {

        // �^�C�g���ȍĐ�
        StartCoroutine(audioManager.PlayBGM(0));

        // �Q�[���J�n��ԂɃZ�b�g
        isGameUp = false;
        isSetUp = false;

        // FloorGenerator�̏���
        SetUpFloorGenerators();

        // �e�W�F�l���[�^�̐������~
        StopGenerators();
    }

    /// <summary>
    /// FloorGenerator�̏���
    /// </summary>
    private void SetUpFloorGenerators()
    {
        for (int i = 0; i < floorGenerators.Length; i++)
        {
            floorGenerators[i].SetUpGenerator(this);
        }
    }

    void Update()
    {
        // �v���C���[���͂��߂ăo���[���𐶐�������
        if (playerController.isFirstGenerateBallon && isSetUp == false)
        {

            // ��������
            isSetUp = true;

            // �e�W�F�l���[�^�̐������X�^�[�g
            ActivateGenerators();

            // �^�C�g���Ȃ��I�����A���C���Ȃ��Đ�
            StartCoroutine(audioManager.PlayBGM(1));
        }
    }

    /// <summary>
    /// �S�[���n�_�̐���
    /// </summary>
    private void GenerateGoal()
    {
        // �S�[���n�_�𐶐�
        GoalChecker goalHouse = Instantiate(goalHousePrefab);


        ////* ��������ǉ� *////

        // �S�[���n�_�̏����ݒ�
        goalHouse.SetUpGoalHouse(this); 

        ////* �����܂� *////

    }

    /// <summary>
    /// �Q�[���I��
    /// </summary>
    public void GameUp()
    {

        // �Q�[���I��
        isGameUp = true;

        // �e�W�F�l���[�^�̐������~
        StopGenerators();
    }

    /// <summary>
    /// �e�W�F�l���[�^���~����
    /// </summary>
    private void StopGenerators()
    {
        for (int i = 0; i < randomObjectGenerators.Length; i++)
        {
            randomObjectGenerators[i].SwitchActivation(false);
        }

        for (int i = 0; i < floorGenerators.Length; i++)
        {
            floorGenerators[i].SwitchActivation(false);
        }
    }


    /// <summary>
    /// �e�W�F�l���[�^�𓮂����n�߂�
    /// </summary>
    private void ActivateGenerators()
    {
        for (int i = 0; i < randomObjectGenerators.Length; i++)
        {
            randomObjectGenerators[i].SwitchActivation(true);
        }

        for (int i = 0; i < floorGenerators.Length; i++)
        {
            floorGenerators[i].SwitchActivation(true);
        }
    }



    ////* ��������V�������\�b�h��ǉ� *////


    /// <summary>
    /// �S�[������
    /// </summary>
    public void GoalClear()
    {
        // �N���A�̋ȍĐ�
        StartCoroutine(audioManager.PlayBGM(2));
    }


    ////* �����܂� *////


}