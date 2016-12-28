using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEGprocessing
{
    public class ListOfEegFiles
    {

        private List<OneFile> _MyFiles;


        public List<OneFile> MyFiles
        {
            get { return this._MyFiles; }
        }

        public ListOfEegFiles()
        {
            this._MyFiles = new List<OneFile>();
        }

        public OneFile GetMyOneEegFileByFilename(string filename)
        {
              foreach (OneFile item in this._MyFiles)
            {
                if (item.filename == filename)
                {
                        return item;                   
                }
            }
            return null;                        
        }



        /// <summary>
        /// этот метод печатает в тестовый файл filenam результаты свертки одного конкретного
        /// файла ЭЭГ, посчитаныые раннее другим методом
        /// </summary>
        /// <param name="filename">Имя файла в который будет все это записывать</param>
        /// <param name="fileIndex">Индекс MyOnFile в массиве  MyFiles</param>
        /// <param name="filterID">Для справки нужно указать в выходном файле Id фильтра с каким сворачивали </param>

        public void PrintToFileByFileIndex(string filename, int fileIndex, int filterID)
        {
            StreamWriter myWriter = new StreamWriter(filename);

            myWriter.WriteLine("Convolution with Filter ID:;#" + filterID.ToString()); //тупо чтобы идеентифицировать с каким фильтром сворачивали

            for (int j = 0; j < this._MyFiles[fileIndex].convolve_chanels[0].Count; j++)
            {

                for (int i = 0; i < MyConst.COUNTOFCHANEL; i++) //пробедим по все каналам. сейчас 21 к примеру
                {
                    myWriter.Write(this._MyFiles[fileIndex].convolve_chanels[i][j]);
                    myWriter.Write(";");
                }
                myWriter.WriteLine();
            }

            myWriter.Close();
        }
    } //myClass

}//namespace

