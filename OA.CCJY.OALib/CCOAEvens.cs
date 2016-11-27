using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.CCJY.OALib
{
    public class CCOAEvens
    {
            //定义事件参数类
            public class CCOAEvenArgs : EventArgs
            {
                public readonly string MessageToRaiseEvent;
                public readonly int ResultToRaiseEvent;
                public CCOAEvenArgs(int result, string message)
                {
                    ResultToRaiseEvent = result;
                    MessageToRaiseEvent = message;
                }
            }

            //定义delegate
            public delegate void CCOAEventHandler(object sender, CCOAEvenArgs e);
            //用event 关键字声明事件对象
            public event CCOAEventHandler CCOAEvent;

            //事件触发方法
            protected virtual void OnCCOAEvent(CCOAEvenArgs e)
            {
                if (CCOAEvent != null)
                    CCOAEvent(this, e);
            }

            //引发事件
            public void RaiseEvent(int result,string keyToRaiseEvent)
            {
                OnCCOAEvent(new CCOAEvenArgs(result,keyToRaiseEvent));
            }

               //订阅事件
            public void Subscribe(CCOAEventHandler handler)
            {
                CCOAEvent +=new CCOAEventHandler(handler);
            }
            //取消订阅事件
            public void UnSubscribe(CCOAEventHandler handler)
            {
                CCOAEvent -=new CCOAEventHandler(handler);
            }
    }
}
