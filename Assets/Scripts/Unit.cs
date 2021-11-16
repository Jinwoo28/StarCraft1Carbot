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
        //스택을 이용해 먼저 지정된 곳을 우선 타겟포지션으로 정하여 스택에 쌓여진 순서대로 이동
        // 스택에 쌓여진 모든 곳을 이동(스택이 비워지면) 이동종료
    }
    private void Unithold()
    {
        //모든 기능을 멈추고 현재 유닛의 자리에 고정
    }
    private void UnitAttack()
    {
        // 기본 상태에서 공격을 받으면 attack모드로 변경

        //마우스 우클릭을 하면
        //일정 거리 내의 가장 가까운 유닛을 공격
        //유닛 지정시 강제로 그 타겟만을 공격
        // 지정한 대상이 공격가능한지, 땅인지 아닌지 판단 필요
    }
    private void Unitpause()
    {
        //하던것을 멈추고 현재 위치에서 대기. 공격을 받으면 공격받은 적을 향해 공격
    }
    private void Unitfix()
    {
        //테란 일꾼에 한해 건물과 일부 유닛 치료가능
    }
    private void Unitpicking()
    {
        //일꾼에 한해 자원채취
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
