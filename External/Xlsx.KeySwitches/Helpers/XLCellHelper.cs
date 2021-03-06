using System.Collections.Generic;
using System.Linq;

using ClosedXML.Excel;

using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;

namespace KeySwitchManager.Xlsx.KeySwitches.Helpers
{
    // ReSharper disable once InconsistentNaming
    public static class XLCellHelper
    {
        public const int MidiNoteOnHeaderCellColor = 0xFCE4D2;
        public const int MidiCcHeaderCellColor = 0xA9D7E1;
        public const int MidiPcHeaderCellColor = 0xBADBA5;
        public const int ExtraHeaderCellColor = 0x909090;

        public static void ActivateRuledLine( IXLStyle style )
        {
            style.Border.TopBorder         = XLBorderStyleValues.Thin;
            style.Border.TopBorderColor    = XLColor.Black;
            style.Border.BottomBorder      = XLBorderStyleValues.Thin;
            style.Border.BottomBorderColor = XLColor.Black;
            style.Border.RightBorder       = XLBorderStyleValues.Thin;
            style.Border.RightBorderColor  = XLColor.Black;
            style.Border.LeftBorder        = XLBorderStyleValues.Thin;
            style.Border.LeftBorderColor   = XLColor.Black;
        }

        public static void SetValidationByBetween(
            IXLWorksheet sheet,
            int row,
            int column,
            string inputMessage,
            int minValue,
            int maxValue )
        {
            IXLWorkbook owner = sheet.Workbook;

            var cell = sheet.Cell( row, column );
            var d = cell.DataValidation;

            d.InputTitle   = $"{minValue}-{maxValue}";
            d.InputMessage = inputMessage;
            d.Decimal.Between( minValue, maxValue );
        }

        public static void SetValidationByList(
            IXLWorksheet sheet,
            int row,
            int column,
            string inputTitle,
            string inputMessage,
            IXLWorksheet listSheet,
            IXLCell begin,
            IXLCell end )
        {
            IXLWorkbook owner = sheet.Workbook;

            var cell = sheet.Cell( row, column );
            var d = cell.DataValidation;

            d.InputTitle   = inputTitle;
            d.InputMessage = inputMessage;
            d.List( listSheet.Range( begin, end ) );
        }

        public static void SetDefaultCellStyle( IXLWorksheet sheet, KeySwitch template, int rowCount = 100 )
        {
            var startRow = SpreadsheetConstants.RowDataBegin;
            var startColumn = SpreadsheetConstants.ColumnMidiMessageBegin;

            var row = startRow;

            var allExtraKeys = template.Articulations.SelectMany( x => x.ExtraData.Keys ).Distinct().ToArray();

            for( var r = 0; r < rowCount; r++ )
            {
                var noteMaxColumn = template.Articulations.Max( x => x.MidiNoteOns.Count );
                var ccMaxColumn = template.Articulations.Max( x => x.MidiControlChanges.Count );
                var pcMaxColumn = template.Articulations.Max( x => x.MidiProgramChanges.Count );

                var column = startColumn;
                column = SetDefaultMidiNoteCellStyle( sheet, row, column, noteMaxColumn );
                column = SetDefaultMidiCcCellStyle( sheet, row, column, ccMaxColumn );
                column = SetDefaultMidiPcCellStyle( sheet, row, column, pcMaxColumn );
                _      = SetDefaultExtraCellStyle( sheet, row, column, allExtraKeys );
                row++;
            }

            sheet.ShowGridLines = false;
        }

        #region Cell style for MIDI Note
        public static int SetDefaultMidiNoteCellStyle( IXLWorksheet sheet, int row, int column, int count = 1 )
        {
            for( var index = 1; index <= count; index++ )
            {
                SetHeaderCell( sheet, SpreadsheetConstants.RowDataHeader, column + 0, MidiNoteOnHeaderCellColor, $"{SpreadsheetConstants.HeaderMidiNote}{index}" );
                SetHeaderCell( sheet, SpreadsheetConstants.RowDataHeader, column + 1, MidiNoteOnHeaderCellColor, $"{SpreadsheetConstants.HeaderMidiVelocity}{index}" );

                SetMidiNoteCellStyle( sheet, row, column + 0 );
                SetMidiVelocityCellStyle( sheet, row, column + 1 );
                column += 2;
            }

            return column;
        }

        public static void SetMidiNoteCellStyle( IXLWorksheet sheet, int row, int column )
        {
            var listSheet = sheet.Workbook.Worksheet( SpreadsheetConstants.DataListDefinitionSheetName );

            var begin = listSheet.Cell(
                SpreadsheetConstants.RowValidationMidiNoteListBegin,
                SpreadsheetConstants.ColumnValidationMidiNoteList );

            var end = listSheet.Cell(
                SpreadsheetConstants.RowValidationMidiNoteListEnd,
                SpreadsheetConstants.ColumnValidationMidiNoteList );

            SetValidationByList(
                sheet,
                row,
                column,
                "MIDI Note",
                "Choose from Drop-down list or input number directly(0-127)\n" +
                "\n" +
                "If don’t use MIDI Note, set Cell value empty.",
                listSheet,
                begin,
                end );

            ActivateRuledLine( sheet.Cell( row, column ).Style );

            sheet.Cell( row, column ).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        public static void SetMidiVelocityCellStyle( IXLWorksheet sheet, int row, int column )
        {
            SetValidationByBetween(
                sheet,
                row,
                column,
                "If don't use MIDI Note on, set cell value empty.",
                0, 127 );

            ActivateRuledLine( sheet.Cell( row, column ).Style );

            sheet.Cell( row, column ).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }
        #endregion

        #region Cell style for MIDI CC
        public static int SetDefaultMidiCcCellStyle( IXLWorksheet sheet, int row, int column, int count = 1 )
        {
            for( var index = 1; index <= count; index++ )
            {
                SetHeaderCell( sheet, SpreadsheetConstants.RowDataHeader, column + 0, MidiCcHeaderCellColor, $"{SpreadsheetConstants.HeaderMidiCc}{index}" );
                SetHeaderCell( sheet, SpreadsheetConstants.RowDataHeader, column + 1, MidiCcHeaderCellColor, $"{SpreadsheetConstants.HeaderMidiCcValue}{index}" );

                SetMidiCcCellStyle( sheet, row, column + 0 );
                SetMidiCcCellStyle( sheet, row, column + 1 );
                column += 2;
            }

            return column;
        }

        public static void SetMidiCcCellStyle( IXLWorksheet sheet, int row, int column )
        {
            SetValidationByBetween(
                sheet,
                row,
                column,
                "If don't use CC set cell value empty",
                0, 127 );

            ActivateRuledLine( sheet.Cell( row, column ).Style );

            sheet.Cell( row, column ).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        #endregion

        #region Cell style for MIDI PC
        public static int SetDefaultMidiPcCellStyle( IXLWorksheet sheet, int row, int column, int count = 1 )
        {
            for( var index = 1; index <= count; index++ )
            {
                SetHeaderCell( sheet, SpreadsheetConstants.RowDataHeader, column + 0, MidiPcHeaderCellColor, $"{SpreadsheetConstants.HeaderPcChannel}{index}" );
                SetHeaderCell( sheet, SpreadsheetConstants.RowDataHeader, column + 1, MidiPcHeaderCellColor, $"{SpreadsheetConstants.HeaderPcData}{index}" );

                SetMidiPcCellStyle( sheet, row, column + 0 );
                SetMidiPcCellStyle( sheet, row, column + 1 );
                column += 2;
            }

            return column;
        }

        public static void SetMidiPcCellStyle( IXLWorksheet sheet, int row, int column )
        {
            SetValidationByBetween(
                sheet,
                row,
                column,
                "If don't use PC set cell value empty",
                0, 127 );

            ActivateRuledLine( sheet.Cell( row, column ).Style );

            sheet.Cell( row, column ).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        #endregion

        #region Cell style for Extra data
        public static int SetDefaultExtraCellStyle( IXLWorksheet sheet, int row, int column, IEnumerable<ExtraDataKey> extraKeys )
        {
            foreach( var key in extraKeys )
            {
                SetHeaderCell(
                    sheet,
                    SpreadsheetConstants.RowDataHeader,
                    column,
                    ExtraHeaderCellColor,
                    SpreadsheetConstants.HeaderExtraPrefix + key.Value
                );
                SetExtraCellStyle( sheet, row, column );
                column++;
            }

            return column;
        }

        public static void SetExtraCellStyle( IXLWorksheet sheet, int row, int column )
        {
            ActivateRuledLine( sheet.Cell( row, column ).Style );
            sheet.Cell( row, column ).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        #endregion

        public static void SetHeaderCell( IXLWorksheet sheet, int row, int column, int color, string text )
        {
            sheet.Cell( row, column ).Value                      = text;
            sheet.Cell( row, column ).Style.Fill.BackgroundColor = XLColor.FromArgb( color );
            ActivateRuledLine( sheet.Cell( row, column ).Style );
            sheet.Cell( row, column ).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

    }
}