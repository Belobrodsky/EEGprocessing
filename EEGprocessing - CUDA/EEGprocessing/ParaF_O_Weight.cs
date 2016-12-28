using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEGprocessing
{
    class ParaF_O_Weight : IComparable
    {
        private Para_ID _indivPara;
        private int _skolkovstrech;

        private float _weight;
        private int _totalamt;


        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            ParaF_O_Weight other = obj as ParaF_O_Weight;
            if (other != null)
                return this._weight.CompareTo(other._weight);
            else
                throw new ArgumentException("Object is not a Temperature");
        }


        public ParaF_O_Weight()
        {
            this._indivPara = new Para_ID();
            this._weight = new float();
            this.skolkovstrech = 0;
        }

        public Para_ID individPara
        {
            set { this._indivPara = value; }
            get { return this._indivPara; }
        }

        public int skolkovstrech
        {
            set { this._skolkovstrech = value; }
            get { return this._skolkovstrech; }
        }

        public int totalamt
        {
            set { this._totalamt = value; }
            get { return this._totalamt; }
        }

        public float weight
        {
            get { return this._weight; }
        }

        public void CalcWeight(int totalcnt)
        {
            this._weight = (float)((float)this._skolkovstrech / (float)totalcnt);
        }

    }

    class ListofPara_F_O_Weight : List<ParaF_O_Weight>
    {

        public void LoadFromListOfSsStatistic(ListOfSsStatistic source)
        {


            int totalamount = 0;

            foreach (OneSsStatistic myOneSS in source)
            {
                totalamount += myOneSS.ListIdPariMaxvalue.Count;

                foreach (paraId_value item in myOneSS.ListIdPariMaxvalue)
                {
                    bool inArray = false;
                    foreach (ParaF_O_Weight paraFOweight in this)
                    {

                        if ((paraFOweight.individPara.filterId == item.para.filterId) && (paraFOweight.individPara.OCfilename == item.para.OCfilename))
                        {
                            paraFOweight.skolkovstrech++;
                            inArray = true;
                        }

                    }

                    if (!inArray)
                    {
                        ParaF_O_Weight temp = new ParaF_O_Weight();
                        temp.individPara = item.para;
                        temp.skolkovstrech = 1;
                        this.Add(temp);
                    }
                }

            } //конец форич

            foreach (ParaF_O_Weight paraFOweight in this)
            {
                paraFOweight.CalcWeight(totalamount);
                paraFOweight.totalamt = totalamount;
            }

            
       
        } //LoadFrom


        public void printWeght(float ff)
        {

            string tempDir = @"Фил-ОС статистика/";

            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }


            string myPath = tempDir + "F_OC_weght_" + (ff * 100).ToString() + ".csv";

            StreamWriter mywr = new StreamWriter(@myPath, false, Encoding.GetEncoding("Windows-1251"));

            mywr.Write("Фильтер ID;");
            mywr.Write("Имя ОС файла;");
            mywr.Write("Сколько раз встречалась;");
            mywr.Write("Сколько всего ;");
            mywr.WriteLine("Вес;");


            foreach (ParaF_O_Weight paraFOweight in this)
            {
                mywr.Write(paraFOweight.individPara.filterId + ";");
                mywr.Write(paraFOweight.individPara.OCfilename + ";");
                mywr.Write(paraFOweight.skolkovstrech + ";");
                mywr.Write(paraFOweight.totalamt + ";");
                mywr.WriteLine(paraFOweight.weight + ";");
            }

            mywr.Close();

        
        } //printWeght


    }


}
