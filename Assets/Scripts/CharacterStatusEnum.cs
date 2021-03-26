using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public enum CharacterStatusEnum
    {
        sick = 1,
        healthy = 2,
        unmasked = 4,
        masked = 8,
        healthyAndMasked = healthy | masked, // 9
        healthyAndUnmasked = healthy | unmasked, // 6
        sickAndMasked = sick | masked, // 8
        sickAndUnmasked = sick | unmasked // 5
    }
}
