using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Effects
{
    internal static class FocusEffects
    {
        public static IFocusEffect CreateBackgroundColorEffect(Color color, Color? baseColor = null)
        {
            throw new NotImplementedException();
            //return (strength) => Camera.main.backgroundColor = Color.Lerp(baseColor ?? new Color(0, 0, 0, 0), color, strength);
        }
    }
}
