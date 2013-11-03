using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    internal class NewQuoteItemEntity
    {
        private double _currentTotal = 0;

        private int _glassTypeID;
        private int _thicknessID;
        private double _totalSqFT;
        private bool _isTempered;
        private bool _isStraightPolish;
        private double _straightPolishLongSide=0;
        private double _straightPolishShortSide=0;

        //Rates
        private double _cutsqftRate = 0;
        private double _temperedSQFT = 0;
        private double _polishStraight = 0;
        private double _polishShape = 0;

        internal int GlassTypeID
        {
            get { return _glassTypeID; }
            set
            {
                _glassTypeID = value;
                CalculateTotal();
            }
        }


        internal string GlassType { get; set; }

        internal int ThicknessID
        {
            get { return _thicknessID; }
            set
            {
                _thicknessID = value;
                CalculateTotal();
            }
        }
        internal string Thickness { get; set; }

        internal double TotalSqFT
        {
            get { return _totalSqFT; }
            set
            {
                _totalSqFT = value;
                CalculateTotal();
            }
        }

        internal double GlassHeight { get; set; }
        internal double GlassWidth { get; set; }

        internal GlassShape Shape { get; set; }

        internal bool IsTempered
        {
            get { return _isTempered; }
            set
            {
                _isTempered = value;
                CalculateTotal();
            }
        }

        internal bool IsStraightPolish
        {
            get { return _isStraightPolish; }
            set
            {
                _isStraightPolish = value;
                CalculateTotal();
            }
        }

        internal double StraightPolishLongSide
        {
            get { return _straightPolishLongSide; }
            set
            {
                _straightPolishLongSide = value;
                CalculateTotal();
            }
        }
        internal double StraightPolishShortSide
        {
            get { return _straightPolishShortSide; }
            set
            {
                _straightPolishShortSide = value;
                CalculateTotal();
            }
        }
        internal double StraightPolishTotalInches { get; set; }

        internal bool IsCustomShapePolish { get; set; }
        internal double CustomPolishLongSide { get; set; }
        internal double CustomPolishShortSide { get; set; }

        internal bool IsMiter { get; set; }
        internal double MiterLongSide { get; set; }
        internal double MiterShortSide { get; set; }
        internal double MiterTotalInches { get; set; }

        internal int Holes { get; set; }
        internal int Notches { get; set; }
        internal int Hinges { get; set; }
        internal int Patches { get; set; }
        internal bool IsLogoRequired { get; set; }
        internal double Insulate { get; set; }

        internal double CurrentTotal
        {
            get { return _currentTotal; }
            set { _currentTotal = value; }
        }

        private void CalculateTotal()
        {
            var result = BusinessLogic.GetRatesByGlassTypeAndThickness(_glassTypeID, _thicknessID);
            if (result == null || result.Tables.Count == 0 || result.Tables[0].Rows.Count == 0)
                return;

            _cutsqftRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.CUTSQFT].ToString());
            _temperedSQFT = double.Parse(result.Tables[0].Rows[0][ColumnNames.TEMPEREDSQFT].ToString());
            _polishStraight = double.Parse(result.Tables[0].Rows[0][ColumnNames.POLISHSTRAIGHT].ToString());
            _polishShape = double.Parse(result.Tables[0].Rows[0][ColumnNames.POLISHSHAPE].ToString());

            if (false==_isTempered)
            {
                _currentTotal = _totalSqFT == 0 ? 0 : (_totalSqFT / 12.0) * _cutsqftRate;
            }
            else
            {
                _currentTotal = _totalSqFT == 0 ? 0 : (_totalSqFT / 12.0) * _temperedSQFT;
            }

            if (true == _isStraightPolish)
            {
                _currentTotal += ((2.0 * StraightPolishLongSide) + (2.0 * StraightPolishShortSide)) * _polishStraight;
            }
            else
            {
                _currentTotal += ((2.0 * 0) + (2.0 * 0)) * _polishStraight;
            }
        }

    }
}
