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
        private int _totalSqFT;
        private bool _isTempered;
        private bool _isMiter;
        private bool _isStraightPolish;
        private bool _isNotch;
        private bool _isHinges;
        private bool _isPatches;
        private bool _isHoles;
        private int _straightPolishLongSide = 0;
        private int _straightPolishShortSide = 0;
        private int _straightPolishTotalInches = 0;
        private int _miterLongSide = 0;
        private int _miterShortSide = 0;

        private bool _isCustomShapePolish;
        private int _customPolishTotalInches;
        private int _miterTotalInches;
        private int _notches;
        private int _hinges;
        private int _patches;
        private int _holes;
        private double _insulateTotalCost;
        private double _cutoutTotal;
        
        //Rates
        private double _cutsqftRate = 0;
        private double _temperedRate = 0;
        private double _polishStraightRate = 0;
        private double _polishShapeRate = 0;
        private double _miterRate = 0;
        private double _notchRate = 0;
        private double _hingeRate = 0;
        private double _patchRate = 0;
        private double _holeRate = 0;

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

        internal int TotalSqFT
        {
            get { return _totalSqFT; }
            set
            {
                _totalSqFT = value;
                CalculateTotal();
            }
        }

        internal int GlassHeight { get; set; }
        internal int GlassWidth { get; set; }

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

        internal int StraightPolishLongSide
        {
            get { return _straightPolishLongSide; }
            set
            {
                _straightPolishLongSide = value;
                CalculateTotal();
            }
        }
        internal int StraightPolishShortSide
        {
            get { return _straightPolishShortSide; }
            set
            {
                _straightPolishShortSide = value;
                CalculateTotal();
            }
        }

        internal int StraightPolishTotalInches
        {
            get { return _straightPolishTotalInches; }
            set
            {
                _straightPolishTotalInches = value;
                CalculateTotal();
            }
        }

        internal bool IsCustomShapePolish
        {
            get { return _isCustomShapePolish; }
            set
            {
                _isCustomShapePolish = value;
                CalculateTotal();
            }
        }
        internal int CustomPolishLongSide { get; set; }
        internal int CustomPolishShortSide { get; set; }

        internal int CustomPolishTotalInches
        {
            get { return _customPolishTotalInches; }
            set
            {
                _customPolishTotalInches = value;
                CalculateTotal();
            }
        }

        internal bool IsMiter
        {
            get { return _isMiter; }
            set
            {
                _isMiter = value;
                CalculateTotal();
            }
        }
        internal int MiterLongSide
        {
            get { return _miterLongSide; }
            set
            {
                _miterLongSide = value;
            }
        }
        internal int MiterShortSide
        {
            get { return _miterShortSide; }
            set
            {
                _miterShortSide = value;
            }
        }
        internal int MiterTotalInches
        {
            get { return _miterTotalInches; }
            set
            {
                _miterTotalInches = value;
                CalculateTotal();
            }
        }

        internal bool IsHoles
        {
            get { return _isHoles; }
            set
            {
                _isHoles = value;
                CalculateTotal();
            }
        }

        internal int Holes
        {
            get { return _holes; }
            set
            {
                _holes = value;
                CalculateTotal();
            }
        }

        public bool IsNotch
        {
            get { return _isNotch; }
            set
            {
                _isNotch = value;
                CalculateTotal();
            }
        }

        internal int Notches
        {
            get { return _notches; }
            set
            {
                _notches = value;
                CalculateTotal();
            }
        }

        public bool IsHinges
        {
            get { return _isHinges; }
            set
            {
                _isHinges = value;
                CalculateTotal();
            }
        }

        internal int Hinges
        {
            get { return _hinges; }
            set
            {
                _hinges = value;
                CalculateTotal();
            }
        }

        public bool IsPatches
        {
            get { return _isPatches; }
            set
            {
                _isPatches = value;
                CalculateTotal();
            }
        }
        internal int Patches
        {
            get { return _patches; }
            set
            {
                _patches = value;
                CalculateTotal();
            }
        }
        internal bool IsLogoRequired { get; set; }

        public double CutoutTotal
        {
            get { return _cutoutTotal; }
            set
            {
                _cutoutTotal = value;
                CalculateTotal();
            }
        }

        internal double InsulateTotalCost
        {
            get { return _insulateTotalCost; }
            set
            {
                _insulateTotalCost = value;
                CalculateTotal();
            }
        }

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
            _temperedRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.TEMPEREDSQFT].ToString());
            _polishStraightRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.POLISHSTRAIGHT].ToString());
            _polishShapeRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.POLISHSHAPE].ToString());
            _miterRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.MITER_RATE].ToString());
            _notchRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.NOTCH_RATE].ToString());
            _hingeRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.HINGE_RATE].ToString());
            _patchRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.PATCH_RATE].ToString());
            _holeRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.HOLE_RATE].ToString());

            if (false == _isTempered)
            {
                _currentTotal = _totalSqFT == 0 ? 0 : _totalSqFT * _cutsqftRate;
            }
            else if (_temperedRate != 0)
            {
                _currentTotal = _totalSqFT == 0 ? 0 : _totalSqFT * _temperedRate;
            }
         

            if (true == _isStraightPolish)
            {
                _currentTotal += _straightPolishTotalInches * _polishStraightRate;
            }

            if (true == _isCustomShapePolish)
            {
                _currentTotal += _customPolishTotalInches * _polishShapeRate;
            }
            if (true == _isMiter)
            {
                _currentTotal += _miterTotalInches * _miterRate;
            }
            if (true == _isNotch && _totalSqFT != 0)
            {
                _currentTotal += _notches * _notchRate;
            }
            if (true == _isHinges && _totalSqFT != 0)
            {
                _currentTotal += _hinges* _hingeRate;
            }
            if (true == _isPatches && _totalSqFT != 0)
            {
                _currentTotal += _patches * _patchRate;
            }

            if (true == _isHoles && _totalSqFT != 0)
            {
                _currentTotal += _holes * _holeRate;
            }

            _currentTotal += _insulateTotalCost;
            _currentTotal += _cutoutTotal;
        }


        internal string GetDescriptionString()
        {
            StringBuilder description = new StringBuilder();

            description.Append(GlassType);

            description.AppendFormat(" " + Environment.NewLine);

            bool isAnyMiscAvailable = false;

            if (_isHoles && _holes >= 0)
            {
                description.AppendFormat(" [Holes - {0}]", _holes);
                isAnyMiscAvailable = true;
            }
            if (_isHinges && _hinges >= 0)
            {
                description.AppendFormat(" [Hinges - {0}]", _hinges);
                isAnyMiscAvailable = true;
            }
            if (_isPatches && _patches >= 0)
            {
                description.AppendFormat(" [Patches - {0}]", _patches);
                isAnyMiscAvailable = true;
            }
            if (_isNotch && _notches>= 0)
            {
                description.AppendFormat(" [Notches - {0}]", _notches);
                isAnyMiscAvailable = true;
            }

            if (isAnyMiscAvailable)
            {
                description.Append(Environment.NewLine);
            }

            bool isStraightPolishDataAvailable = false;
            if (_isStraightPolish && _straightPolishLongSide > 0 && _straightPolishShortSide > 0 && _straightPolishTotalInches >0)
            {
                description.AppendFormat(" [Straight Polish -> Log Sides - {0}, Short Sides - {1}, Total (in) - {2}]", _straightPolishLongSide, _straightPolishShortSide, _straightPolishTotalInches);
                isStraightPolishDataAvailable = true;
            }

            if (_isCustomShapePolish && _customPolishTotalInches> 0 )
            {
                description.AppendFormat(" [Custom Polish -> Total (in) - {0}]", _customPolishTotalInches);
                isStraightPolishDataAvailable = true;
            }

            if (isStraightPolishDataAvailable)
            {
                description.Append(Environment.NewLine);
            }

            bool isMiterDataAvailable = false;
            if (_isMiter && _miterLongSide > 0 && _miterShortSide > 0 && _miterTotalInches > 0)
            {
                description.AppendFormat(" [Miter -> Log Sides - {0}, Short Sides - {1}, Total (in) - {2}]", _miterLongSide, _miterShortSide, _miterTotalInches);
                isMiterDataAvailable = true;
            }
            if (isMiterDataAvailable)
            {
                description.Append(Environment.NewLine);
            }

            if (IsLogoRequired)
            {
                description.AppendFormat(" [Logo]");
            }

            return description.ToString();
        }
    }
}
