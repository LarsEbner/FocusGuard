using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.UISwap
{
    /// <summary>
    /// Interface for applying effects to refocus.
    /// </summary>
    internal interface IFocusEffect
    {
        /// <summary>
        /// Applies the focus effect according to a given strength.
        /// A strength of 0 means the effect should be cleared.
        /// A strength of 1 means the effect should be fully applied.
        /// </summary>
        /// <param name="strength">A value between 0 and 1 regarding how much the effect should be applied.</param>
        public void ApplyEffect(double strength);
    }
}
