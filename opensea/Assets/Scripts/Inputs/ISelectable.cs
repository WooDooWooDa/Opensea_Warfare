using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Inputs
{
    public interface ISelectable
    {
        void OnSelect();
        void OnDeselect();
    }
}
