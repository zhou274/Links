using System;

namespace Links.Scripts
{
    [Flags]
    public enum NodeType
    {
        Static         = 0,
        Rotatable      = 1,
        MoveHorizontal = 1 << 1,
        MoveVertical   = 1 << 2,
        Moveable       =  MoveHorizontal | MoveVertical,
        MoveableRotatable = Rotatable | Moveable
    }
}