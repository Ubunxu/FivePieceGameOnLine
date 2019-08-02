using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    [Serializable]
    public class ByteBuffer
    {
        private byte[] buffer;
        private int readIndex = 0;
        private int writeIndex = 0;
        private int _type = 0;
        public static ByteBuffer CreateByteBufferForLenth(int leng)
        {
            ByteBuffer bf = new ByteBuffer(-1, leng);
            return bf;
        }
        public static ByteBuffer CreateByteBufferType(int type,int leng = 128)
        {
            ByteBuffer bf = new ByteBuffer(-1, leng);
            bf.writeInt(type);
            bf.Type = type;
            return bf;
        }
        public ByteBuffer(int __type = -1, int length = 1024)
        {
            this.buffer = new byte[length];
            //this._type = __type;
            //if (this._type != -1)
            //{
            //    this.writeInt(this._type);
            //}
        }
        public void ExpansionCapacity(int size)
        {
            byte[] b = new byte[size];
            Array.Copy(this.buffer, 0, b, 0, this.Length);
            Array.Clear(this.buffer, 0, this.Length);
            this.buffer = b;
        }
        public int Type
        {
            get { return this._type; }
            set { this._type = value; }
        }
        public int Length
        {
            get { return this.writeIndex; }
            set { this.writeIndex = value; }
        }
        public int Available
        {
            get { return this.writeIndex - this.readIndex; }
        }
        public byte[] getBuffer()
        {
            return this.buffer;
        }
        public void writeInt(int value)
        {
            byte[] intbs = BitConverter.GetBytes(value);
            this.writeBytes(intbs);
        }

        public void writeFloat(float value)
        {
            byte[] floatbs = BitConverter.GetBytes(value);
            this.writeBytes(floatbs);
        }

        public void writeBytes(byte[] bytes)
        {
            Array.Copy(bytes, 0, this.buffer, this.writeIndex, bytes.Length);
            this.writeIndex += bytes.Length;
        }
        public void writeBytes(byte[] bytes, int index, int length)
        {
            Array.Copy(bytes, index, this.buffer, this.writeIndex, length);
            this.writeIndex += length;
        }
        public void writeBuffer(ByteBuffer buf, int index)
        {
            this.writeBytes(buf.getBuffer(), index, buf.Length - index);
        }

        public void writeString(string str)
        {
            byte[] strbs = Encoding.UTF8.GetBytes(str);
            this.writeInt(strbs.Length);
            this.writeBytes(strbs);
        }

        public int readInt()
        {
            int value = BitConverter.ToInt32(this.buffer, this.readIndex);
            this.readIndex += 4;
            return value;
        }

        public string readString()
        {
            int len = this.readInt();
            string sv = Encoding.UTF8.GetString(this.buffer, readIndex, len);
            this.readIndex += len;
            return sv;
        }
        public void Clear()
        {
            Array.Clear(this.buffer, 0, this.writeIndex);
            this.writeIndex = 0;
            this.readIndex = 0;
        }

        public void Send(Socket socket)
        {
            //try
            //{
            if (socket != null && socket.Connected)
            {
                byte[] lengbs = BitConverter.GetBytes(this.Length);
                socket.Send(lengbs, 0, lengbs.Length, 0);//SocketFlags.None
                socket.Send(this.buffer, 0, this.Length, SocketFlags.None);
            }
           // }
            //catch(Exception e)
            //{

           // }
        }
        public ByteBuffer readBuffer()
        {
            int len = this.readInt();
            ByteBuffer buff = new ByteBuffer(-1, len);
            buff.writeBytes(this.buffer, this.readIndex, len);
            this.readIndex += len;
            return buff;
        }
    }
}
