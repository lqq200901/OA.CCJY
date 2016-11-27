using System;
using System.Linq;
using System.ComponentModel;
using JumpKick.HttpLib;
using System.Threading;

namespace OA.CCJY.OALib.Item
{
    public class Attach:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static bool AutoDown = true;
        public static string AttachSavePath = "";
        public static string AttachTempPath="";
        public static string CCOADomainUrl = "";

        public int AID { get; set; }

        static string[] typeStr=new string[]{"正文", "附件"};
        int type = 0;
        
        public int TypeNO
        {
            get { return type; }
            set { type = value; }
        }
        public string Type {
            get
            {
                return typeStr[type];
            }
            set
            {
                type=(byte)typeStr.ToList().IndexOf(value.Trim());
            }
        }
        public string Name { get; set; }

        private string path = string.Empty;
        public string Path {
            get { return path; }
            set { path = value; if (AutoDown)  Download(); }
        }
        public bool State { get; set; }

        private int totalBytes;
        public int TotalBytes
        {
            get
            {
                return totalBytes;
            }

            set
            {
                totalBytes = value;
            }
        }

        private int bytesCopied;
        public int BytesCopied
        {
            get
            {
                return bytesCopied;
            }

            set
            {
                bytesCopied = value;
            }
        }

        private void Download()
        {
            if(Name!=string.Empty&&Path!=string.Empty)
            {
                Http.Get(this.path).DownloadTo(AttachSavePath+Name , onProgressChanged: (bytesCopied, totalBytes) =>
                {
                    SynchronizationContext.Current.Post(new SendOrPostCallback((s) =>
                    {
                        var ss = ((string)s).Split('|');
                        int copied = int.Parse(ss[0]);
                        int total = int.Parse(ss[1]);

                        BytesCopied = copied;
                        TotalBytes = total;
                    }), bytesCopied.ToString() + "|" + totalBytes.ToString());
                },
                onSuccess: (headers) =>
                {
                    SynchronizationContext.Current.Post(new SendOrPostCallback((s) =>
                    {
                        State = (bool)s;
                    }), true);
                }
                ).Go();
            }
        }

        public static bool Exists(Attach attach)
        {
            return System.IO.File.Exists(attach.Path);
        }

        public bool Exists()
        {
            return Exists(this);
        }

        public void OnPropertyChanged(string msg)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(msg));
            }
        }
    }
}
