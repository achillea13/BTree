using System;
using System.Collections.Generic;


namespace Ltm.ActionTree
{
    public delegate void TriggerFunction(params object[] objs);
    public static class TriggerFunManager
    {
        protected static IDictionary<int, TriggerFunction> _dictTriggerFunc = new Dictionary<int, TriggerFunction>();
        public static void AddEvent(int eventType, TriggerFunction func)
        {
            if (_dictTriggerFunc.ContainsKey(eventType))
            {
                // 已经加入了，替换
                SimpleLog.Log("TriggerNode::AddEvent 已经存在相同的值，即将替换");
            }
            _dictTriggerFunc[eventType] = func;
        }
        public static void RemoveEvent(int eventType)
        {
            if ( false == _dictTriggerFunc.ContainsKey(eventType) )
            {
                // 不存在指定类型的事件
                SimpleLog.Log("TriggerNode::RemoveEvent 不存在指定的键");
                return;
            }

            _dictTriggerFunc.Remove(eventType);
        }

        public static void DoEvent(int eventType, params object[] args)
        {
            if (false == _dictTriggerFunc.ContainsKey(eventType))
            {
                // 不存在指定类型的事件
                SimpleLog.Log("TriggerNode::DoEvent 不存在指定的键");
                return;
            }

            _dictTriggerFunc[eventType](args);
        }
    }

    public class TriggerNode : INode
    {
        protected int _eventType = 0;


        public TriggerNode(int eventType)
            : base()
        {
            this._eventType = eventType;
        }

        public TriggerNode(int eventType, string name)
            : base(name)
        {
            this._eventType = eventType;
        }

        public TriggerNode(int eventType, TriggerFunction func)
            : base()
        {
            this._eventType = eventType;

            TriggerFunManager.AddEvent(eventType, func);
        }

        public TriggerNode(int eventType, TriggerFunction func, string name)
            : base(name)
        {
            this._eventType = eventType;

            TriggerFunManager.AddEvent(eventType, func);
        }

        public override void Visit(NodeInput input, NodeOutput output)
        {
            if (output.data == null)
            {
                _status = NodeStatus.FAILED;
            }
            else
            {
                TriggerFunManager.DoEvent(_eventType, output.data);
                _status = NodeStatus.SUCCESS;
            }

        }

    }
}
