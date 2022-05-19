using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private string horizontal = "Horizontal";    // �L�[���͗p�̕�����w��


    ////* ��������ǉ� *////

    private string jump = "Jump";        // �L�[���͗p�̕�����w��

    ////* �����܂� *////


    private Rigidbody2D rb;                      // �R���|�[�l���g�̎擾�p
    private Animator anim;

    private float scale;                         // �����̐ݒ�ɗ��p����

    public float moveSpeed;                      // �ړ����x


    ////* ��������ǉ� *////


    public float jumpPower;                      // �W�����v�E���V��


    ////* �����܂� *////


    void Start()
    {
        // �K�v�ȃR���|�[�l���g���擾���ėp�ӂ����ϐ��ɑ��
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        scale = transform.localScale.x;
    }


    ////* �������烁�\�b�h���Q�ǉ� *////


    void Update()
    {

        // �W�����v
        if (Input.GetButtonDown(jump))
        {    // InputManager �� Jump �̍��ڂɓo�^����Ă���L�[���͂𔻒肷��
            Jump();
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


    ////* �����܂� *////


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
    }
}