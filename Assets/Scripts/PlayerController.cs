using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private string horizontal = "Horizontal";    // �L�[���͗p�̕�����w��
    private string jump = "Jump";

    private Rigidbody2D rb;                      // �R���|�[�l���g�̎擾�p
    private Animator anim;

    private float scale;                         // �����̐ݒ�ɗ��p����
    private float limitPosX = 9.5f;       �@     // �������̐����l
    private float limitPosY = 4.45f;         �@  // �c�����̐����l

    public float moveSpeed;                      // �ړ����x
    public float jumpPower;                      // �W�����v�E���V��

    public bool isGrounded;

    public GameObject[] ballons;                 // GameObject�^�̔z��B�C���X�y�N�^�[����q�G�����L�[�ɂ��� Ballon �Q�[���I�u�W�F�N�g���Q�A�T�C������


    ////* ��������ϐ��̐錾���T�ǉ� *////


    public int maxBallonCount;                   // �o���[���𐶐�����ő吔

    public Transform[] ballonTrans;              // �o���[���̐����ʒu�̔z��

    public GameObject ballonPrefab;              // �o���[���̃v���t�@�u

    public float generateTime;                   // �o���[���𐶐����鎞��

    public bool isGenerating;                    // �o���[���𐶐������ǂ����𔻒肷��Bfalse �Ȃ琶�����Ă��Ȃ���ԁBtrue �͐������̏��


    ////* �����܂� *////


    [SerializeField, Header("Linecast�p �n�ʔ��背�C���[")]
    private LayerMask groundLayer;


    void Start()
    {
        // �K�v�ȃR���|�[�l���g���擾���ėp�ӂ����ϐ��ɑ��
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        scale = transform.localScale.x;


        ////* ��������ǉ� *////


        // �z��̏�����(�o���[���̍ő吶���������z��̗v�f����p�ӂ���)
        ballons = new GameObject[maxBallonCount];


        ////* �����܂� *////


    }


    void Update()
    {

        // �n�ʐڒn  Physics2D.Linecast���\�b�h�����s���āAGround Layer�ƃL�����̃R���C�_�[�Ƃ��ڒn���Ă��鋗�����ǂ������m�F���A�ڒn���Ă���Ȃ� true�A�ڒn���Ă��Ȃ��Ȃ� false ��߂�
        isGrounded = Physics2D.Linecast(transform.position + transform.up * 0.4f, transform.position - transform.up * 0.9f, groundLayer);

        // Scene�r���[�� Physics2D.Linecast���\�b�h��Line��\������
        Debug.DrawLine(transform.position + transform.up * 0.4f, transform.position - transform.up * 0.9f, Color.red, 1.0f);


        ////* ��������C�� *////

        // ballons�ϐ��̍ŏ��̗v�f�̒l����ł͂Ȃ��Ȃ� = �o���[�����P���������Ƃ��̗v�f�ɒl���������� = �o���[�����P����Ȃ�
        if (ballons[0] != null)
        {         // <=�@���@������ύX����@��

            ////* �����܂� *////

            // �W�����v
            if (Input.GetButtonDown(jump))
            {
                Jump();
            }

            // �ڒn���Ă��Ȃ�(�󒆂ɂ���)�ԂŁA�������̏ꍇ
            if (isGrounded == false && rb.velocity.y < 0.15f)
            {

                // �����A�j�����J��Ԃ�
                anim.SetTrigger("Fall");
            }
        }
        else
        {
            Debug.Log("�o���[�����Ȃ��B�W�����v�s��");
        }



        // Velocity.y �̒l�� 5.0f �𒴂���ꍇ(�W�����v��A���ŉ������ꍇ)
        if (rb.velocity.y > 5.0f)
        {

            // Velocity.y �̒l�ɐ�����������(���������ɏ��őҋ@�ł��Ă��܂����ۂ�h������)
            rb.velocity = new Vector2(rb.velocity.x, 5.0f);
        }


        ////* ��������ǉ� *////


        // �n�ʂɐڒn���Ă��āA�o���[�����������ł͂Ȃ��ꍇ
        if (isGrounded == true && isGenerating == false)
        {

            // Q�{�^������������
            if (Input.GetKeyDown(KeyCode.Q))
            {

                // �o���[�����P�쐬����
                StartCoroutine(GenerateBallon());
            }
        }


        ////* �����܂� *////


    }

    /// <summary>
    /// �W�����v�Ƌ󒆕��V
    /// </summary>
    private void Jump()
    {

        // �L�����̈ʒu��������ֈړ�������(�W�����v�E���V)
        rb.AddForce(transform.up * jumpPower);

        // Jump(Up + Mid) �A�j���[�V�������Đ�����
        anim.SetTrigger("Jump");
    }


    void FixedUpdate()
    {
        // �ړ�
        Move();
    }


    /// <summary>
    /// �ړ�
    /// </summary>
    private void Move()
    {

        // ����(��)�����ւ̓��͎�t
        float x = Input.GetAxis(horizontal);

        // x �̒l�� 0 �ł͂Ȃ��ꍇ = �L�[���͂�����ꍇ
        if (x != 0)
        {

            // velocity(���x)�ɐV�����l�������Ĉړ�
            rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);

            // temp �ϐ��Ɍ��݂� localScale �l����
            Vector3 temp = transform.localScale;

            // ���݂̃L�[���͒l x �� temp.x �ɑ��
            temp.x = x;

            // �������ς��Ƃ��ɏ����ɂȂ�ƃL�������k��Ō����Ă��܂��̂Ő����l�ɂ���            
            if (temp.x > 0)
            {

                //  ������0�����傫����΂��ׂ�1�ɂ���
                temp.x = scale;

            }
            else
            {
                //  ������0������������΂��ׂ�-1�ɂ���
                temp.x = -scale;
            }

            // �L�����̌������ړ������ɍ��킹��
            transform.localScale = temp;

            // �ҋ@��Ԃ̃A�j���̍Đ����~�߂āA����A�j���̍Đ��ւ̑J�ڂ��s��
            anim.SetBool("Idle", false);
            anim.SetFloat("Run", 0.5f);

        }
        else
        {
            //  ���E�̓��͂��Ȃ������牡�ړ��̑��x��0�ɂ��Ē�~����
            rb.velocity = new Vector2(0, rb.velocity.y);

            //  ����A�j���̍Đ����~�߂āA�ҋ@��Ԃ̃A�j���̍Đ��ւ̑J�ڂ��s��
            anim.SetFloat("Run", 0.0f);
            anim.SetBool("Idle", true);
        }

        // ���݂̈ʒu��񂪈ړ��͈͂̐����͈͂𒴂��Ă��Ȃ����m�F����B�����Ă�����A�����͈͓��Ɏ��߂�
        float posX = Mathf.Clamp(transform.position.x, -limitPosX, limitPosX);
        float posY = Mathf.Clamp(transform.position.y, -limitPosY, limitPosY);

        // ���݂̈ʒu���X�V(�����͈͂𒴂����ꍇ�A�����ňړ��͈̔͂𐧌�����)
        transform.position = new Vector2(posX, posY);
    }


    ////* �������烁�\�b�h���P�ǉ� *////


    /// <summary>
    /// �o���[������
    /// </summary>
    /// <returns></returns>
    private IEnumerator GenerateBallon()
    {

        // ���ׂĂ̔z��̗v�f�Ƀo���[�������݂��Ă���ꍇ�ɂ́A�o���[���𐶐����Ȃ�
        if (ballons[1] != null)
        {
            yield break;
        }

        // ��������Ԃɂ���
        isGenerating = true;

        // �P�߂̔z��̗v�f����Ȃ�
        if (ballons[0] == null)
        {
            // 1�ڂ̃o���[�������𐶐����āA1�Ԗڂ̔z��֑��
            ballons[0] = Instantiate(ballonPrefab, ballonTrans[0]);
        }
        else
        {
            // 2�ڂ̃o���[�������𐶐����āA2�Ԗڂ̔z��֑��
            ballons[1] = Instantiate(ballonPrefab, ballonTrans[1]);
        }

        // �������ԕ��ҋ@
        yield return new WaitForSeconds(generateTime);

        // ��������ԏI���B�ēx�����ł���悤�ɂ���
        isGenerating = false;
    }


    ////* �����܂� *////


}