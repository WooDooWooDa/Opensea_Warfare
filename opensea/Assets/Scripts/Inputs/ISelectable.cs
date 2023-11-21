using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Inputs
{
    public interface ISelectable
    {
        void OnSelect();
        void OnDeselect();
    }
}
