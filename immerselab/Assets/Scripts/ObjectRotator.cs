using System;
using UnityEngine;

public class ObjectRotator : IDisposable
{
    private readonly float _speed;
    private readonly Transform _rotationObject;
    private readonly InputReceiver _inputReceiver;

    public ObjectRotator(Transform rotationObject, float speed, InputReceiver inputReceiver){
        _speed = speed;
        _rotationObject = rotationObject;
        _inputReceiver = inputReceiver;
        _inputReceiver.MoveEvent += Rotate;
    }

    public void Dispose() => _inputReceiver.MoveEvent += Rotate;

    private void Rotate(Vector3 touchDeltaPosition){
        var rotateVectorBySpeed = touchDeltaPosition * _speed;
        var rotation = Quaternion.Euler(-rotateVectorBySpeed.y, rotateVectorBySpeed.x, 0);
        
        _rotationObject.rotation = rotation * _rotationObject.rotation;
    }
}
