using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text txtScore;

    [SerializeField]
    private Text txtInfo;

    [SerializeField]
    private CanvasGroup canvasGroupInfo;

    [SerializeField]
    private ResultPopUp resultPopUpPrefab;

    [SerializeField]
    private Transform canvasTran;

    [SerializeField]
    private Button btnInfo;


    ////* ��������ǉ� *////


    [SerializeField]
    private Button btnTitle;

    [SerializeField]
    private Text lblStart;

    [SerializeField]
    private CanvasGroup canvasGroupTitle;

    private Tweener tweener;


    ////* �����܂� *////


    /// <summary>
    /// �X�R�A�\�����X�V
    /// </summary>
    /// <param name="score"></param>
    public void UpdateDisplayScore(int score)
    {
        txtScore.text = score.ToString();
    }

    /// <summary>
    /// �Q�[���I�[�o�[�\��
    /// </summary>
    public void DisplayGameOverInfo()
    {

        // InfoBackGround �Q�[���I�u�W�F�N�g�̎��� CanvasGroup �R���|�[�l���g�� Alpha �̒l���A1�b������ 1 �ɕύX���āA�w�i�ƕ�������ʂɌ�����悤�ɂ���
        canvasGroupInfo.DOFade(1.0f, 1.0f);

        // ��������A�j���[�V���������ĕ\��
        txtInfo.DOText("Game Over...", 1.0f);

        btnInfo.onClick.AddListener(RestartGame);
    }

    /// <summary>
    /// ResultPopUp�̐���
    /// </summary>
    public void GenerateResultPopUp(int score)
    {
        // ResultPopUp �𐶐�
        ResultPopUp resultPopUp = Instantiate(resultPopUpPrefab, canvasTran, false);

        // ResultPopUp �̐ݒ���s��
        resultPopUp.SetUpResultPopUp(score);
    }

    /// <summary>
    /// �^�C�g���֖߂�
    /// </summary>
    public void RestartGame()
    {

        // �{�^�����烁�\�b�h���폜(�d���N���b�N�h�~)
        btnInfo.onClick.RemoveAllListeners();

        // ���݂̃V�[���̖��O���擾
        string sceneName = SceneManager.GetActiveScene().name;

        canvasGroupInfo.DOFade(0f, 1.0f)
            .OnComplete(() => {
                Debug.Log("Restart");
                SceneManager.LoadScene(sceneName);
            });
    }


    ////* �V�������\�b�h���S�ǉ��B�������� *////

    private void Start()
    {

        // �^�C�g���\��
        SwitchDisplayTitle(true, 1.0f);

        // �{�^����OnClick�C�x���g�Ƀ��\�b�h��o�^
        btnTitle.onClick.AddListener(OnClickTitle);
    }

    /// <summary>
    /// �^�C�g���\���i�������R�����g�ŏ����Ă݂܂��傤�j
    /// </summary>
    public void SwitchDisplayTitle(bool isSwitch, float alpha)
    {
        if (isSwitch) canvasGroupTitle.alpha = 0;

        canvasGroupTitle.DOFade(alpha, 1.0f).SetEase(Ease.Linear).OnComplete(() => {
            lblStart.gameObject.SetActive(isSwitch);
        });

        if (tweener == null)
        {
            // Tap Start�̕������������_�ł�����
            tweener = lblStart.gameObject.GetComponent<CanvasGroup>().DOFade(0, 1.0f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            tweener.Kill();
        }
    }

    /// <summary>
    /// �^�C�g���\�����ɉ�ʂ��N���b�N�����ۂ̏���
    /// </summary>
    private void OnClickTitle()
    {
        // �{�^���̃��\�b�h���폜���ďd���^�b�v�h�~
        btnTitle.onClick.RemoveAllListeners();

        // �^�C�g�������X�ɔ�\��
        SwitchDisplayTitle(false, 0.0f);

        // �^�C�g���\����������̂Ɠ���ւ��ŁA�Q�[���X�^�[�g�̕�����\������
        StartCoroutine(DisplayGameStartInfo());
    }

    /// <summary>
    /// �Q�[���X�^�[�g�\���i�������R�����g�ŏ����Ă݂܂��傤�j
    /// </summary>
    /// <returns></returns>
    public IEnumerator DisplayGameStartInfo()
    {
        yield return new WaitForSeconds(0.5f);

        canvasGroupInfo.alpha = 0;
        canvasGroupInfo.DOFade(1.0f, 0.5f);
        txtInfo.text = "Game Start!";

        yield return new WaitForSeconds(1.0f);
        canvasGroupInfo.DOFade(0f, 0.5f);

        canvasGroupTitle.gameObject.SetActive(false);
    }

    ////* �����܂� *////

}