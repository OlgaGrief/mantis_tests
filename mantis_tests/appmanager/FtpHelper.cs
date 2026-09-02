using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.FtpClient;

namespace mantis_tests
{
    public class FtpHelper : HelperBase
    {
        private FtpClient client;
        public FtpHelper(ApplicationManager manager) : base(manager)
        {
            client = new FtpClient();
            client.Host = "127.0.0.1";
            //client.Port = 4555;
            client.Credentials = new System.Net.NetworkCredential("mantis", "mantis");
            client.Connect();
        }

        // Метод для резервного копирования файла на FTP-сервере
        public void BackupFile(string path)
        {
            String backupPath = path + ".bak"; // создаем путь для резервной копии файла
            if (client.FileExists(backupPath))
            {
                return; // если резервная копия уже существует, то выходим из метода
            }
            client.Rename(path, backupPath); // переименовываем файл на сервере в резервную копию
        }

        // Метод для восстановления файла из резервной копии на FTP-сервере
        public void RestoreBackupFile(string path)
        {
            String backupPath = path + ".bak"; // создаем путь для резервной копии файла
            if (!client.FileExists(backupPath))
            {
                return; // если резервная копия не существует, то выходим из метода
            }
            if (client.FileExists(backupPath))
            {
                client.DeleteFile(path); // удаляем текущий файл на сервере, если он существует
            }
            client.Rename(backupPath, path); // переименовываем резервную копию в исходный файл на сервере
        }

        // Метод для загрузки файла на FTP-сервер
        public void Upload(String path, Stream localFile)
        {
            if (client.FileExists(path))
            {
                client.DeleteFile(path); // удаляем текущий файл на сервере, если он существует
            }
            // Загружаем локальный файл на сервер
            using (Stream ftpStream = client.OpenWrite(path))
            {
                byte[] buffer = new byte[8 * 1024]; // создаем буфер для чтения данных из локального файла
                int count = localFile.Read(buffer, 0, buffer.Length); // читаем данные из локального файла в буфер
                while (count > 0)
                {
                    ftpStream.Write(buffer, 0, count); // записываем данные из буфера в поток FTP-сервера
                    count = localFile.Read(buffer, 0, buffer.Length); // читаем данные из локального файла в буфер
                }
            }
        }
    }
}
