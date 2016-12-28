using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEGprocessing
{
    /// <summary>
    /// Этот класс нуже для второго критерия ранжирования
    /// </summary>
    public class OneFilterVsMain : IComparable
    {

        private int _filterID;
        private string _mainfilename;
        private List<float> _fitnessArray; //здесь будет массив значение целевой функции пары
        private float _fitness;
        private int _currgeneration;

        public int currgeneration
        {
            set { this._currgeneration = value; }
            get { return this._currgeneration; }
        
        }

        

        public OneFilterVsMain()
        {
            this._filterID = new int();
            this._mainfilename = "";
            this._fitnessArray = new List<float>();
            this._fitness = new float();
            this._currgeneration = new int();
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            OneFilterVsMain other = obj as OneFilterVsMain;
            if (other != null)
                return this._fitness.CompareTo(other._fitness);
            else
                throw new ArgumentException("Object is not a Temperature");
        }

        public void CalcFiltess()
        {
            this._fitness = this._fitnessArray.Average();
        }

        public List<float> fitnessArray
        {
            //set { this._fitness = value; }
            get { return this._fitnessArray; }

        }
        public int filterID
        {

            set { this._filterID = value; }
            get { return this._filterID; }
        }

        public string mainfilename
        {
            set { this._mainfilename = value; }
            get { return this._mainfilename; }
        }


    }

}
