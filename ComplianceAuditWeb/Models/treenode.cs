using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplianceAuditWeb.Models
{
    public class treenode
    {
            public string id;
            public string text;
            public string icon;
            public State state;
            public List<treenode> children;
            public string categorytype;

        //public static treenode NewNode(string id)
        //{
        //    return new treenode()
        //    {
        //        id = id,
        //        text = string.Format("Node {0}", id),
        //        children = new List<treenode>()
        //    };
        //}
    }
        public class State
    {
            public bool opened = false;
            public bool disabled = false;
            public bool selected = false;

            public State(bool Opened, bool Disabled, bool Selected)
            {
                opened = Opened;
                disabled = Disabled;
                selected = Selected;
            }
        }
}