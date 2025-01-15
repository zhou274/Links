using System.Collections;
using System.Collections.Generic;

namespace GameTemplate.Scripts
{
    public interface ILevelScript<TLevel>
    {
        TLevel level { get; set; }

        void LoadLevel();
    }
}