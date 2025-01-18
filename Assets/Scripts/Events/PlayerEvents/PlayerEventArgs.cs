using UnityEngine;
using System;

namespace Events.PlayerEvents
{
    public class MovementEventArgs : EventArgs
    {
        public Vector3 Value { get; set; }
    }
}