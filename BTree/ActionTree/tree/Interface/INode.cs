using System;
using System.Collections.Generic;


namespace Ltm.ActionTree
{
    enum NodeStatus
    {
        READY = 1,

        RUNNING,

        SUCCESS,

        FAILED,
    }

    public abstract class INode
    {
        protected NodeStatus _status;
        public NodeStatus status
        {
            get{return _status;}
        }

        private string _name;
        public string name
        {
            get { return _name; }
        }

        public INode()
        {
            _name = this.GetType().Name;
        }
        public INode(string nodeName)
        {
            _name = this.GetType().Name + nodeName;
        }

        public abstract void Visit( NodeInput input, NodeOutput output );

        public virtual void Step()
        {
            if (status != NodeStatus.RUNNING)
            {
                this.Reset();
            }
        }

        public virtual void Reset()
        {
            if (status != NodeStatus.READY)
            {
                _status = NodeStatus.READY;
            }
        }
    }
}
