using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEGprocessing
{
    public class OneFilter : IComparable
    {
        private int _ID;
        private int _motherID;//у фильтров которые поколения 0 и больше
        private int _fatherID;//будут у фильтров которые поколения 0 и больше
        private string _mutation;
        private string _filterFilename;
        private int _countOfFilterData;
        private List<float> _data;
        private List<float> _furier;

        private float _averOfDiffAver_for_rating; //Среднее среди средних разниц
        
        private float _averOfMaxDiff_for_rating; // Среднее среди максимальных разниц

        private float _averOfABSMAX12_for_rating; // Среднее среди значение типа abs(max1-max2)


        private float _norm;

        //public int CompareTo(object obj)
        //{
        //    if (obj == null) return 1;
        //    OneFilter otherFilter= obj as OneFilter;
        //    if (otherFilter != null)
        //        return this._averOfDiffAver_for_rating.CompareTo(otherFilter._averOfDiffAver_for_rating);
        //    else
        //        throw new ArgumentException("Object is not a Temperature");
        //}

        //public int CompareTo(object obj)
        //{
        //    if (obj == null) return 1;
        //    OneFilter otherFilter = obj as OneFilter;
        //    if (otherFilter != null)
        //        return this._averOfDiffAver_for_rating.CompareTo(otherFilter._averOfDiffAver_for_rating);
        //    else
        //        throw new ArgumentException("Object is not a Temperature");
        //}

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            OneFilter otherFilter = obj as OneFilter;
            if (otherFilter != null)
            {

                switch ((int)MyConst.whatIsFitness)
                { 
                    case (int)1:
                     return this._averOfDiffAver_for_rating .CompareTo(otherFilter._averOfDiffAver_for_rating);
                        break;
                    case (int)2:
                        return this._averOfMaxDiff_for_rating.CompareTo(otherFilter._averOfMaxDiff_for_rating);
                        break;

                    case (int)3:
                        return this._averOfABSMAX12_for_rating.CompareTo(otherFilter._averOfABSMAX12_for_rating);
                        break;
                    default:
                        return this._averOfMaxDiff_for_rating.CompareTo(otherFilter._averOfMaxDiff_for_rating);
                        break;
                           
                }
            
            }

                //case  MyConst.whatIsFitness:
            //    return this._averOfMaxDiff_for_rating.CompareTo(otherFilter._averOfMaxDiff_for_rating);
           
            
            else
                throw new ArgumentException("Object is not a Temperature");
        }



        public OneFilter()
        {
            this._data = new List<float>();
            this._furier = new List<float>();
            this._mutation = "";

            this._averOfABSMAX12_for_rating = new float();
            this._averOfDiffAver_for_rating = new float();
            this._averOfMaxDiff_for_rating = new float();
        }
        //  public float[] mex

        public float averOfDiffAver_for_rating
        {
            set { this._averOfDiffAver_for_rating = value; }
            get { return this._averOfDiffAver_for_rating; }
        }

        public float averofABSMAX12_for_rating
        {
            set { this._averOfABSMAX12_for_rating = value; }
            get { return this._averOfABSMAX12_for_rating; }
        
        }


        public float norm
        {
            set { this._norm = value; }
            get { return this._norm; }
        }

        public string mutation
        {
            set { this._mutation = value; }
            get { return this._mutation; }
        }

        public float averOfMaxDiff_for_rating
        {
            set { this._averOfMaxDiff_for_rating = value; }
            get { return this._averOfMaxDiff_for_rating; }

        }
        /// <summary>
        /// Делаем нудевое среднее и единичную норму для каждого конкретно фильтра
        /// </summary>
        public void Normalize()
        {
            float sr = this.data.Average();
            //для 0 среднего
            for (int i = 0; i < this.data.Count; i++)
            {
                this.data[i] = this.data[i] - sr;
            }




            float A = new float();

            A = (float)0;
            //для нахождения энергии начального
            for (int i = 0; i < this.data.Count; i++)
            {
                A = A + this.data[i] * this.data[i];
            }


            //для нормализации каждого
            for (int i = 0; i < this.data.Count; i++)
            {
                this.data[i] = this.data[i] / ((float)Math.Sqrt(A));
            }


            //для контрольной проверки
            A = 0;
            for (int i = 0; i < this.data.Count; i++)
            {
                A = A + this.data[i] * this.data[i];
            }
            this._norm = A;
        }

        public void LoadFilterFromFile(string filename)
        {

            this._filterFilename = filename;
            StreamReader myReader = new StreamReader(filename);

            string line = "";
            int i = 0;
            float value = new float();

            while (line != null)
            {
                line = myReader.ReadLine();

                if (line != null)
                {
                    try
                    {
                        value = float.Parse(line);
                    }
                    catch (Exception)
                    {
                        value = MyConst.ERRORVALUE;
                    }
                    if (value != MyConst.ERRORVALUE)
                    {
                        i++;
                        //в этом месте я надеюсь все распарсино правильно
                        //поэтому можем добавлять 
                        this._data.Add(value);
                    }

                }  //if line!=null;
            } //while line!=null

            this._countOfFilterData = i;
            myReader.Close();
        }

        public List<float> data
        {
            get { return this._data; }
            set { this._data = value; }
        }

        public List<float> furier
        {
            get { return this._furier; }
        }


        public void CalcFurier()
        {
            this._furier = MyConst.CsharpDFT(this._data);

        }

        public string filterFilename
        {
            get { return this._filterFilename; }
            set { this._filterFilename = value; }
        }

        public int countOfFilterData
        {
            get { return this._countOfFilterData; }
            set { this._countOfFilterData = value; }
        }

        public int ID
        {

            get { return this._ID; }
            set { this._ID = value; }
        }

        public int fatherID
        {

            get { return this._fatherID; }
            set { this._fatherID = value; }
        }

        public int motherID
        {
            get { return this._motherID; }
            set { this._motherID = value; }

        }
    }
}
