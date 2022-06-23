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
    private float limitPosX = 9.5f;              // �������̐����l
    private float limitPosY = 4.45f;             // �c�����̐����l

    public bool isFirstGenerateBallon;          // ���߂ăo���[���𐶐��������𔻒肷�邽�߂̕ϐ�

    public float moveSpeed;                      // �ړ����x
    public float jumpPower;                      // �W�����v�E���V��

    public bool isGrounded;

    public GameObject[] ballons;                 // GameObject�^�̔z��B�C���X�y�N�^�[����q�G�����L�[�ɂ��� Ballon �Q�[���I�u�W�F�N�g���Q�A�T�C������
    public int maxBallonCount;                   // �o���[���𐶐�����ő吔
    public Transform[] ballonTrans;              // �o���[���̐����ʒu�̔z��
    public GameObject ballonPrefab;              // �o���[���̃v���t�@�u

    public float generateTime;                   // �o���[���𐶐����鎞��
    public bool isGenerating;                    // �o���[���𐶐������ǂ����𔻒肷��Bfalse �Ȃ琶�����Ă��Ȃ���ԁBtrue �͐������̏��

    public float knockbackPower;                 // �G�ƐڐG�����ۂɐ�����΂�����
    public int coinPoint;                        // �R�C�����l������Ƒ�����|�C���g�̑���


    ///* ��������ǉ� *////


    public UIManager uiManager;


    ////* �����܂Œǉ� *////


    [SerializeField, Header("Linecast�p �n�ʔ��背�C���[")]
    private LayerMask groundLayer;

    [SerializeField]
    private StartChecker startChecker;


    void Start()
    {
        // �K�v�ȃR���|�[�l���g���擾���ėp�ӂ����ϐ��ɑ��
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        scale = transform.localScale.x;

        // �z��̏�����(�o���[���̍ő吶���������z��̗v�f����p�ӂ���)
        ballons = new GameObject[maxBallonCount];
    }

    void Update()
    {

        // �n�ʐڒn  Physics2D.Linecast���\�b�h�����s���āAGround Layer�ƃL�����̃R���C�_�[�Ƃ��ڒn���Ă��鋗�����ǂ������m�F���A�ڒn���Ă���Ȃ� true�A�ڒn���Ă��Ȃ��Ȃ� false ��߂�
        isGrounded = Physics2D.Linecast(transform.position + transform.up * 0.4f, transform.position - transform.up * 0.9f, groundLayer);

        // Scene�r���[�� Physics2D.Linecast���\�b�h��Line��\������
        Debug.DrawLine(transform.position + transform.up * 0.4f, transform.position - transform.up * 0.9f, Color.red, 1.0f);

        // ballons�ϐ��̍ŏ��̗v�f�̒l����ł͂Ȃ��Ȃ� = �o���[�����P���������Ƃ��̗v�f�ɒl���������� = �o���[�����P����Ȃ�
        if (ballons[0] != null)
        {

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
            //  ���E�̓��͂��Ȃ������牡�ړ��̑��x��0�ɂ��ăs�^�b�Ǝ~�܂�悤�ɂ���
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

        // isFirstGenerateBallon �ϐ��̒l�� false�A�܂�A�Q�[�����J�n���Ă���A�܂��o���[�����P����������Ă��Ȃ��Ȃ�
        if (isFirstGenerateBallon == false)
        {

            // ����o���[���������s�����Ɣ��f���Atrue �ɕύX���� = ����ȍ~�̓o���[���𐶐����Ă��Aif ���̏����𖞂����Ȃ��Ȃ�A���̏����ɂ͓���Ȃ�
            isFirstGenerateBallon = true;

            Debug.Log("����̃o���[������");

            // startChecker �ϐ��ɑ������Ă��� StartChecker �X�N���v�g�ɃA�N�Z�X���āASetInitialSpeed ���\�b�h�����s����
            startChecker.SetInitialSpeed();
        }

        // �P�߂̔z��̗v�f����Ȃ�
        if (ballons[0] == null)
        {

            // 1�ڂ̃o���[�������𐶐����āA1�Ԗڂ̔z��֑��
            ballons[0] = Instantiate(ballonPrefab, ballonTrans[0]);
            ballons[0].GetComponent<Ballon>().SetUpBallon(this);

        }
        else
        {

            // 2�ڂ̃o���[�������𐶐����āA2�Ԗڂ̔z��֑��
            ballons[1] = Instantiate(ballonPrefab, ballonTrans[1]);
            ballons[1].GetComponent<Ballon>().SetUpBallon(this);

        }

        // �������ԕ��ҋ@
        yield return new WaitForSeconds(generateTime);

        // ��������ԏI���B�ēx�����ł���悤�ɂ���
        isGenerating = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

        // �ڐG�����R���C�_�[�����Q�[���I�u�W�F�N�g��Tag��Enemy�Ȃ� 
        if (col.gameObject.tag == "Enemy")
        {

            // �L�����ƓG�̈ʒu���狗���ƕ������v�Z
            Vector3 direction = (transform.position - col.transform.position).normalized;

            // �G�̔��Α��ɃL�����𐁂���΂�
            transform.position += direction * knockbackPower;
        }
    }

    /// <summary>
    /// �o���[���j��
    /// </summary>
    public void DestroyBallon()
    {

        // TODO ����A�o���[�����j�󂳂��ۂɁu���ꂽ�v�悤�Ɍ�����A�j�����o��ǉ�����

        if (ballons[1] != null)
        {
            Destroy(ballons[1]);
        }
        else if (ballons[0] != null)
        {
            Destroy(ballons[0]);
        }
    }

    // IsTrigger���I���̃R���C�_�[�����Q�[���I�u�W�F�N�g��ʉ߂����ꍇ�ɌĂяo����郁�\�b�h
    private void OnTriggerEnter2D(Collider2D col)
    {

        // �ʉ߂����R���C�_�[�����Q�[���I�u�W�F�N�g�� Tag �� Coin �̏ꍇ
        if (col.gameObject.tag == "Coin")
        {

            // �ʉ߂����R�C���̃Q�[���I�u�W�F�N�g�̎��� Coin �X�N���v�g���擾���Apoint �ϐ��̒l���L�����̎��� coinPoint �ϐ��ɉ��Z
            coinPoint += col.gameObject.GetComponent<Coin>().point;


            ////* ��������ǉ� *////


            uiManager.UpdateDisplayScore(coinPoint);


            ////* �����܂Œǉ� *////


            // �ʉ߂����R�C���̃Q�[���I�u�W�F�N�g��j�󂷂�
            Destroy(col.gameObject);
        }
    }
}