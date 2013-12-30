using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private int _totalSqFTCharged;
        private int _quantity;
        private bool _isTempered;
        private bool _isMiter;
        private bool _isStraightPolish;
        private bool _isNotch;
        private bool _isHinges;
        private bool _isPatches;
        private bool _isHoles;
        private bool _isInsulation;
        private bool _isCutout;
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
        private double _pricePerUnit;
        private string _shape;
        private string _thickness;

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
        private int _minimumTotalSqft = 0;

        internal ObservableCollection<CutoutData> _allCutoutData = new ObservableCollection<CutoutData>();

        internal int GlassTypeID
        {
            get { return _glassTypeID; }
            set
            {
                _glassTypeID = value;
                CalculateTotal();
            }
        }

        public int MinimumTotalSqft 
        {
            get { return _minimumTotalSqft; }
            set { _minimumTotalSqft = value; }
        }

        internal string GlassType { get; set; }

        internal double PricePerUnit { get { return _pricePerUnit; } }

        internal int ThicknessID
        {
            get { return _thicknessID; }
            set
            {
                _thicknessID = value;
                CalculateTotal();
            }
        }

        internal double TotalSqFT
        {
            get { return _totalSqFT; }
            set
            {
                _totalSqFT = value;
                CalculateTotal();
            }
        }

        internal int TotalSqFTCharged
        {
            get { return _totalSqFTCharged; }
            set
            {
                _totalSqFTCharged = value;
                CalculateTotal();
            }
        }

        internal int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                CalculateTotal();
            }
        }
        internal int GlassHeight { get; set; }
        internal string GlassHeightFraction { get; set; }

        internal int GlassWidth { get; set; }
      
        internal int GlassWidthCharged { get; set; }
        internal int GlassHeightCharged { get; set; }
        
        internal string GlassWidthFraction { get; set; }

        internal string Shape
        {
            get { return _shape; }
            set { _shape = value; }
        }

        internal string Thickness
        {
            get { return _thickness; }
            set { _thickness = value; }
        }

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

        public bool IsCutout
        {
            get { return _isCutout; }
            set
            {
                _isCutout = value;
                CalculateTotal();
            }
        }
        public double CutoutTotal
        {
            get { return _cutoutTotal; }
            set
            {
                _cutoutTotal = value;
                CalculateTotal();
            }
        }

        public bool IsInsulation
        {
            get { return _isInsulation; }
            set
            {
                _isInsulation = value;
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

        internal InsulationDetails GlassType1;
        internal InsulationDetails GlassType2;

        private void CalculateTotal()
        {
            var result = BusinessLogic.GetRatesByGlassTypeAndThickness(_glassTypeID, _thicknessID);
            if (result == null || result.Tables.Count == 0 || result.Tables[0].Rows.Count == 0)
                return;

            _cutsqftRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.CUTSQFT] == DBNull.Value ? "0" : result.Tables[0].Rows[0][ColumnNames.CUTSQFT].ToString());
            _temperedRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.TEMPEREDSQFT] == DBNull.Value ? "0" : result.Tables[0].Rows[0][ColumnNames.TEMPEREDSQFT].ToString());
            _polishStraightRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.POLISHSTRAIGHT] == DBNull.Value ? "0" : result.Tables[0].Rows[0][ColumnNames.POLISHSTRAIGHT].ToString());
            _polishShapeRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.POLISHSHAPE] == DBNull.Value ? "0" : result.Tables[0].Rows[0][ColumnNames.POLISHSHAPE].ToString());
            _miterRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.MITER_RATE] == DBNull.Value ? "0" : result.Tables[0].Rows[0][ColumnNames.MITER_RATE].ToString());
            _notchRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.NOTCH_RATE] == DBNull.Value ? "0" : result.Tables[0].Rows[0][ColumnNames.NOTCH_RATE].ToString());
            _hingeRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.HINGE_RATE] == DBNull.Value ? "0" : result.Tables[0].Rows[0][ColumnNames.HINGE_RATE].ToString());
            _patchRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.PATCH_RATE] == DBNull.Value ? "0" : result.Tables[0].Rows[0][ColumnNames.PATCH_RATE].ToString());
            _holeRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.HOLE_RATE] == DBNull.Value ? "0" : result.Tables[0].Rows[0][ColumnNames.HOLE_RATE].ToString());
            
            _minimumTotalSqft = int.Parse(result.Tables[0].Rows[0][ColumnNames.MinimumTotalSqft].ToString());

            if(_totalSqFTCharged < _minimumTotalSqft)
            {
                _totalSqFTCharged = _minimumTotalSqft;
            }

            if (false == _isTempered)
            {
                _currentTotal = _totalSqFTCharged == 0 ? 0 : _totalSqFTCharged * _cutsqftRate;
                //_currentTotal = _totalSqFT == 0 ? 0 : _totalSqFT * _cutsqftRate;
            }
            else if (_temperedRate != 0)
            {
                _currentTotal = _totalSqFTCharged == 0 ? 0 : _totalSqFTCharged * _temperedRate;
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
            if (true == _isNotch && _totalSqFTCharged != 0)
            {
                _currentTotal += _notches * _notchRate;
            }
            if (true == _isHinges && _totalSqFTCharged != 0)
            {
                _currentTotal += _hinges * _hingeRate;
            }
            if (true == _isPatches && _totalSqFTCharged != 0)
            {
                _currentTotal += _patches * _patchRate;
            }

            if (true == _isHoles && _totalSqFTCharged != 0)
            {
                _currentTotal += _holes * _holeRate;
            }

            if (true == _isInsulation && _totalSqFTCharged != 0)
            {
                _currentTotal += _insulateTotalCost;
            }
            if (true == _isCutout && _totalSqFTCharged != 0)
            {
                _currentTotal += _cutoutTotal;
            }
            _pricePerUnit = _currentTotal;
            _currentTotal = _currentTotal * _quantity;
        }

        internal string GetDescriptionString()
        {
            StringBuilder description = new StringBuilder();

            if (_isInsulation)
            {
                string isTemp1 = GlassType1.IsTempered ? "Temp" : "Not Temp";
                string isTemp2 = GlassType2.IsTempered ? "Temp" : "Not Temp";
                description.AppendFormat("[Insulation: ({0}-{1}-{2}) ({3}-{4}-{5}) {6} SqFt]", GlassType1.GlassType, GlassType1.Thickness, isTemp1, GlassType2.GlassType, GlassType2.Thickness, isTemp2, GlassType1.SqFt);
                description.Append(Environment.NewLine);
            }
            else
            {
                description.Append(GlassType);
            }

            if (false == string.IsNullOrEmpty(_shape))
            {
                description.AppendFormat(" [{0}]", _shape);
            }
            if(false== string.IsNullOrEmpty(_thickness))
            {
                description.AppendFormat(" [{0}]", _thickness);
            }

            if (_isTempered && _isInsulation == false)
            {
                description.Append(" [Temp]");
            }

            if (_isCutout)
            {
                description.AppendFormat(" [Cutout: {0}]", _allCutoutData.Count);
            }

            //if (_isStraightPolish && _straightPolishLongSide > 0 && _straightPolishShortSide > 0 && _straightPolishTotalInches > 0)
                if (_isStraightPolish == true)
            {
                description.AppendFormat(" [Polish: -> Long {0}, Short {1}, (in) {2}]", _straightPolishLongSide, _straightPolishShortSide, _straightPolishTotalInches);
            }

            if (_isCustomShapePolish && _customPolishTotalInches > 0)
            {
                description.AppendFormat(" [CusPolish: (in) {0}]", _customPolishTotalInches);
            }

            description.AppendFormat(" " + Environment.NewLine);

            bool isAnyMiscAvailable = false;

            if (_isHoles && _holes >= 0)
            {
                description.AppendFormat(" [Holes: {0}]", _holes);
                isAnyMiscAvailable = true;
            }
            if (_isHinges && _hinges >= 0)
            {
                description.AppendFormat(" [Hinges: {0}]", _hinges);
                isAnyMiscAvailable = true;
            }
            if (_isPatches && _patches >= 0)
            {
                description.AppendFormat(" [Patches: {0}]", _patches);
                isAnyMiscAvailable = true;
            }
            if (_isNotch && _notches >= 0)
            {
                description.AppendFormat(" [Notches: {0}]", _notches);
                isAnyMiscAvailable = true;
            }
            if (_isMiter && _miterLongSide > 0 && _miterShortSide > 0 && _miterTotalInches > 0)
            {
                description.AppendFormat(" [Miter: Log {0}, Short {1}, (in) {2}]", _miterLongSide, _miterShortSide, _miterTotalInches);
                isAnyMiscAvailable = true;
            }

            if (isAnyMiscAvailable)
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
