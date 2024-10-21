using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanScript : MonoBehaviour
{
    [SerializeField] HumanColliderScript[] sideColliders;
    bool isDead;
    DoorScript doorScript;

    [SerializeField] float moveSpeed;
    [SerializeField] bool randomSpeedFlag;
    [SerializeField] float min;
    [SerializeField] float max;
    [SerializeField] float moveRatio;//�h�A���牓���������̊J�Ŏ~�܂�悤��
    [SerializeField] float doorLength;//�h�A�ɓ��������炢�̋����ł͎~�܂�Ȃ��悤��
    [SerializeField] float badRatio;//���҂ɂȂ銄��
    [SerializeField] float minBodySize;
    [SerializeField] float maxBodySize;
    [SerializeField] float noppoRatio;//�f�u���m�b�|�̊���
    Rigidbody2D mRigidbody;
    public SpriteRenderer bodyColor;
    Vector2 dif;
    bool getOnFlag;//��Ԃ������t���O
    TrainManager trainManager;
    Vector2 moveVec;
    public float humanMoveRatio;
    bool thisBadMan;


    public string debugText;
    // Start is called before the first frame update
    void Start()
    {
        doorScript = FindAnyObjectByType<DoorScript>();
        trainManager = FindAnyObjectByType<TrainManager>();
        mRigidbody = GetComponent<Rigidbody2D>();
        mRigidbody.velocity = new Vector2(0, 0);

        if (randomSpeedFlag)
        {
            moveSpeed = Random.Range(min, max);
        }
        float randomNum = Random.Range(0, 100);
        //���҂Ȃ�F��ς���
        if (randomNum <= badRatio)
        {
            thisBadMan = true;
            bodyColor.color = Color.red;
        }
        if (thisBadMan)
        {
            humanMoveRatio /= 2;
        }

        //�m�b�|���f�u����������A�T�C�Y�����Z
        float isNoppo = Random.Range(0, 100);
        Vector3 newSize = transform.localScale;
        if (isNoppo <= noppoRatio)
        {
            newSize.y += Random.Range(minBodySize, maxBodySize);
        }
        else
        {
            newSize.x += Random.Range(minBodySize, maxBodySize);

        }
        transform.localScale = newSize;

    }

    // Update is called once per frame
    void Update()
    {
        //�ړ��������Ƀh�A�Ɍ��������x���v�Z
        moveVec = doorScript.transform.position - transform.position;
        moveVec = moveVec.normalized * moveSpeed;
        //�h�A�܂ł̋���
        dif = this.transform.position - doorScript.transform.position;
        //humanMoveRatio = Mathf.Clamp((dif.magnitude * dif.magnitude) / moveRatio, 0.4f, 1.0f);
        //debugText = "";
        Move();
        //GotoTrain();
        Dead();
        //debugText += mRigidbody.velocity;
        debugText = "\nlength=" + dif.magnitude + "\nRaito=" + (dif.magnitude * dif.magnitude) / moveRatio + "\nhumanMoveRatio=" + humanMoveRatio;
    }
    //�ړ�����
    void Move()
    {
        if (isDead) { return; }
        if (getOnFlag) { return; }
        if (dif.magnitude < doorLength)
        {
            mRigidbody.velocity = moveVec;
            return;
        }

        if (thisBadMan)
        {
            if (doorScript.GetOpenRatio() < humanMoveRatio)
            {
                mRigidbody.velocity = moveVec/2;

                return;
            }
        }
        else 
        {
            if (doorScript.GetOpenRatio() < humanMoveRatio)
            {
                mRigidbody.velocity = new Vector2(0, 0);

                return;
            }
        }
        
        mRigidbody.velocity = moveVec;

    }
    //���S����
    void Dead()
    {
        if (getOnFlag) { return; }
        //if (thisBadMan) { return; }
        //�T�C�h�̃R���C�_�[���ǂ������h�A�ɐG��Ă���
        if (sideColliders[0].GetIsHit() && sideColliders[1].GetIsHit())
        {
            if (!isDead)
            {
                Debug.Log("Dead");

                if (thisBadMan)
                {
                    trainManager.BadDeadCount();
                }
                else
                {
                    trainManager.DeadCount();
                }

            }
            isDead = true;
            mRigidbody.velocity = new Vector2(0, 0);
            Destroy(this.gameObject);
        }
    }

    //��ԏ���
    void GotoTrain()
    {
        if (isDead) { return; }
        //if (thisBadMan) { return; }


        //�ǂ̊p�x����ł��Ή��o����悤�Ƀh�A����̋����œ������������m����
        if (dif.magnitude <= doorScript.GetGetonLength())
        {
            if (!getOnFlag)
            {
                if (thisBadMan)
                {
                    trainManager.BadTrainIn();
                }
                else
                {
                    trainManager.TrainIn();
                }

            }
            getOnFlag = true;
            //�e�X�g�p�ɐl���폜����
            Destroy(this.gameObject);

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "InsideCollider")
        {
            if (!getOnFlag)
            {
                if (thisBadMan)
                {
                    trainManager.BadTrainIn();
                }
                else
                {
                    trainManager.TrainIn();
                }

            }
            getOnFlag = true;
            //�e�X�g�p�ɐl���폜����
            Destroy(this.gameObject);
        }
    }
}
