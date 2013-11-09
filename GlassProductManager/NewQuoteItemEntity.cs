﻿using System;
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
        private int _straightPolishLongSide = 0;
        private int _straightPolishShortSide = 0;
        private int _straightPolishTotalInches = 0;

        private bool _isCustomShapePolish;
        private int _customPolishTotalInches;
        private int _miterTotalInches;
        private int _notches;
        private int _hinges;
        private int _patches;
        private double _insulateTotalCost;
        private double _cutoutTotal;
        
        //Rates
        private double _cutsqftRate = 0;
        private double _temperedSQFT = 0;
        private double _polishStraight = 0;
        private double _polishShape = 0;
        private double _miterRate = 0;
        private double _notchRate = 0;
        private double _hingeRate = 0;
        private double _patchRate = 0;
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
        internal int MiterLongSide { get; set; }
        internal int MiterShortSide { get; set; }
        internal int MiterTotalInches
        {
            get { return _miterTotalInches; }
            set
            {
                _miterTotalInches = value;
                CalculateTotal();
            }
        }

        internal int Holes { get; set; }

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
            _temperedSQFT = double.Parse(result.Tables[0].Rows[0][ColumnNames.TEMPEREDSQFT].ToString());
            _polishStraight = double.Parse(result.Tables[0].Rows[0][ColumnNames.POLISHSTRAIGHT].ToString());
            _polishShape = double.Parse(result.Tables[0].Rows[0][ColumnNames.POLISHSHAPE].ToString());
            _miterRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.MITER_RATE].ToString());
            _notchRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.NOTCH_RATE].ToString());
            _hingeRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.HINGE_RATE].ToString());
            _patchRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.PATCH_RATE].ToString());

            if (false == _isTempered)
            {
                _currentTotal = _totalSqFT == 0 ? 0 : _totalSqFT * _cutsqftRate;
            }
            else
            {
                _currentTotal = _totalSqFT == 0 ? 0 : _totalSqFT * _temperedSQFT;
            }

            if (true == _isStraightPolish)
            {
                _currentTotal += _straightPolishTotalInches * _polishStraight;
            }

            if (true == _isCustomShapePolish)
            {
                _currentTotal += _customPolishTotalInches * _polishShape;
            }
            if (true == _isMiter)
            {
                _currentTotal += _miterTotalInches * _miterRate;
            }
            if (true == _isNotch)
            {
                _currentTotal += _notches * _notchRate;
            }
            if (true == _isHinges)
            {
                _currentTotal += _hinges* _hingeRate;
            }
            if (true == _isPatches)
            {
                _currentTotal += _patches * _patchRate;
            }
            _currentTotal += _insulateTotalCost;
            _currentTotal += _cutoutTotal;
        }

    }
}
