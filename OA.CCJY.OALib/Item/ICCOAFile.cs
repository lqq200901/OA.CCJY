using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.CCJY.OALib.Item
{
    public interface ICCOAFile
    {
        /// <summary>
        /// 文件编号
        /// </summary>
        int ID { get; }

        /// <summary>
        /// 文件标题
        /// </summary>
        string Title { get;  }

        /// <summary>
        /// 文件状态
        /// </summary>
        int State { get; set; }

        /// <summary>
        /// 发件人
        /// </summary>
        int Sender { get;  }

        bool SetSender(int senderID);

        bool SetSender(string sender);

    }
}
