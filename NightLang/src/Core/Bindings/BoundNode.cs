using System;

namespace NightL
{
    abstract class BoundNode
    {
        public abstract BoundNodeKind NodeKind { get; }
    }
}