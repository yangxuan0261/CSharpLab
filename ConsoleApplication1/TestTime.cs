using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;

class TestTime {

    internal class CNode {
        public string mName;
        public WeakReference mWeakRef;
        public DateTime mTime;
    };

    public static void test1() {
        List<CNode> nodeList = new List<CNode>();
        CNode node = new CNode();
        node.mName = "aaa";
        node.mTime = DateTime.Now;
        nodeList.Add(node);

        Thread.Sleep(1000);

        node = new CNode();
        node.mName = "bbb";
        node.mTime = DateTime.Now;
        nodeList.Add(node);

        Thread.Sleep(1000);

        node = new CNode();
        node.mName = "ccc";
        node.mTime = DateTime.Now;
        nodeList.Add(node);

        nodeList.Sort((CNode n1, CNode n2) => {
            return n2.mTime.CompareTo(n1.mTime);
        });
        
        for (int i = 0; i < nodeList.Count; i++) {
            node = nodeList[i];
            Console.WriteLine("name:{0}, time:{1}", node.mName, node.mTime);
        }
    }
}
