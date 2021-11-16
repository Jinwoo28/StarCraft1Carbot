using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private int Unitdamage = 0;
    private float UnitSpeed = 0;

    private bool hold = false;
    private bool move = false;
    private bool attack = false;
    private bool pause = false;
    private bool patrol = false;
    private bool fix = false;
    private bool picking = false;

    protected enum Mode
    {
        Move,
        Patrol,
        Attack,
        pause,
        hold,

        build,
        fix,
        picking
    }

    Mode Unitmode = Mode.pause;

    protected void modeChange()
    {
        if (hold) Unitmode = Mode.hold;
        else
        {
            if (move) Unitmode = Mode.Move;

            else
            {
                if (attack) Unitmode = Mode.Attack;
                else
                {
                    if (pause) Unitmode = Mode.pause;
                    else if (patrol) Unitmode = Mode.pause;
                }
            }
        }
    }

    protected void ModeAction()
    {
        switch (Unitmode)
        {
            case Mode.Move:
                break;
            case Mode.Patrol:
                break;
            case Mode.hold:
                break;
            case Mode.Attack:
                break;
            case Mode.pause:
                break;
        }
    }

    private void UnitMove(Vector2 TargetPos)
    {

    }
    private void UnitPatrol()
    {
        //������ �̿��� ���� ������ ���� �켱 Ÿ������������ ���Ͽ� ���ÿ� �׿��� ������� �̵�
        // ���ÿ� �׿��� ��� ���� �̵�(������ �������) �̵�����
    }
    private void Unithold()
    {
        //��� ����� ���߰� ���� ������ �ڸ��� ����
    }
    private void UnitAttack()
    {
        // �⺻ ���¿��� ������ ������ attack���� ����

        //���콺 ��Ŭ���� �ϸ�
        //���� �Ÿ� ���� ���� ����� ������ ����
        //���� ������ ������ �� Ÿ�ٸ��� ����
        // ������ ����� ���ݰ�������, ������ �ƴ��� �Ǵ� �ʿ�
    }
    private void Unitpause()
    {
        //�ϴ����� ���߰� ���� ��ġ���� ���. ������ ������ ���ݹ��� ���� ���� ����
    }
    private void Unitfix()
    {
        //�׶� �ϲۿ� ���� �ǹ��� �Ϻ� ���� ġ�ᰡ��
    }
    private void Unitpicking()
    {
        //�ϲۿ� ���� �ڿ�ä��
    }


    protected void SetDamage(int damage_)
    {
        Unitdamage = damage_;
    }

    protected void SetSpeed(float speed_)
    {
        UnitSpeed = speed_;
    }

}
