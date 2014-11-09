using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake2
{
    public class BaseObjectList : List<BaseGameObject>
    {
        public new void Add(BaseGameObject obj)
        {
            base.Add(obj);
            obj.Initialize();
        }
    }
}
