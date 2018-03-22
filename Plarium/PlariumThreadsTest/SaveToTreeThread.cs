using System;
using System.IO.Pipes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace PlariumThreadsTest
{
    public class InfoEventArgs : EventArgs
    {
        public readonly Info ItemInfo;
        public InfoEventArgs(Info itemInfo)
        {
            ItemInfo = itemInfo;
        }
    }
    
    class SaveToTreeThread : ThreadClass
    {
        public event EventHandler<InfoEventArgs> FileScanned;
        public event EventHandler<EventArgs> Finished;

        override protected void ThreadFunc()
        {
            NamedPipeServerStream pipeServer =
                new NamedPipeServerStream(Settings.TreePipeName, PipeDirection.In);
            pipeServer.WaitForConnection();
            BinaryFormatter binFormat = new BinaryFormatter();            

            while (pipeServer.IsConnected)
            {
                Info fi = null;
                try { 
                    fi = (Info)binFormat.Deserialize(pipeServer); 
                }
                catch (SerializationException)
                {
                    //гасим ошибку десериализации последнего элемента из-за ошибки позиционирования в пайпе
                    //гашение исключения принято из-за невозможности установить позицию в пайпе (Position = 0)
                    //сама десериализация проходит успешно
                    //в реальном продукте возможно использование более сложной схемы получения данных до десериализации
                }
                if (fi != null && FileScanned != null)
                    FileScanned.Invoke(this, new InfoEventArgs(fi));                
            }
            pipeServer.Close();

            if (Finished != null)
                Finished.Invoke(this, new EventArgs());
        }
    }
}
