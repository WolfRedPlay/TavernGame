using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    [SerializeField] HingeJoint _door;
    [SerializeField] float _baseMotorSpeed = 200f;
    [SerializeField] string _tag = "Player";


    List<GameObject> _objectsInsideTrigger = new List<GameObject>();


    public void Initialize()
    {
        JointMotor motor = _door.motor;
        motor.targetVelocity = Mathf.Abs(_baseMotorSpeed);
        motor.force = Mathf.Abs(_baseMotorSpeed / 10);
        _door.motor = motor;
        _door.useMotor = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tag))
        {

            _objectsInsideTrigger.Add(other.gameObject);

            if (_objectsInsideTrigger.Count == 1)
            {
                JointMotor motor = _door.motor;
                motor.targetVelocity = -Mathf.Abs(_baseMotorSpeed);
                _door.motor = motor;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_tag))
        {

            _objectsInsideTrigger.Remove(other.gameObject);

            if (_objectsInsideTrigger.Count == 0)
            {
                JointMotor motor = _door.motor;
                motor.targetVelocity = Mathf.Abs(_baseMotorSpeed);
                _door.motor = motor;
            }
        }
    }
}
