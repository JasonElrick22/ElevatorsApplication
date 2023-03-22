
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;



namespace Logic.ElevatorLogic
{

    public class ElevatorLogic
    {
        private IElevatorLogic _elevatorLogic = new DAL.Functions.ElevatorFunctions();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public async Task<Boolean> CreateEquation(string _equationString, string x_value, string y_value)
        {
            try
            {
                DateTime nowDateTime = DateTime.Now;
                var result = await _equation.AddEquation(_equationString, DateTime.Now, x_value, y_value);
                if (result.id > 0)
                {
                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                log.Error(ex.InnerException);
                return false;
            }
        }
 
        public async Task<List<Equation>> GetAllEquations()
        {
            List<Equation> equations = new List<Equation>();
            try
            {
                equations = await _equation.GetAllEquations();
            }
            catch(Exception ex)
            {
                log.Error(ex.InnerException);
            }
            return equations;
        }
        //---------------------------------------------------------------
        public static double ProcessEquation(string _equation, string x)
        {

            char chOpen = '(';
            char chClose = ')';

            var _equationArray = _equation.Split();
            string x_holder = x;
            string _replacedEquation = "";
            string _finalEquation = "";

            _equation = _equation.Replace("x", x);
            _equation = _equation.Replace("Y", "");
            _equation = _equation.Replace("y", "");
            _equation = _equation.Replace("=", "");
            _equation = _equation.Replace(" ", "");

            //Redundant
            int freqOpenBracket = _equation.Where(x => (x == chOpen)).Count();
            int freqCloseBracket = _equation.Where(x => (x == chClose)).Count();
            int bracketIndex = freqOpenBracket - 1;

            double rollingCount = 0;

            #region Calculate bracket

            // Fist attempt at recursive approach to brackets
            try
            {
                // Calculate Exponential
                if (_equation.Contains("^")) {

                    char[] _cArray = _equation.ToCharArray();

                    char startInd = _cArray[_equation.IndexOf("^") - 1];
                    char startEnd= _cArray[_equation.IndexOf("^") +1];

                    string I1 = startInd.ToString();
                    string I2 = startEnd.ToString();

                    double i1 = Convert.ToDouble(I1); 
                    double i2 = Convert.ToDouble(I2);

                    
                    int startIndex = _equation.IndexOf('^', (_equation.IndexOf('^')));
                    int endIndex = startIndex + 2;
                    string _subEquation = _equation.Substring(startIndex-1, endIndex-2);
                    
                    double temp = Math.Pow(i1, i2);
                    _replacedEquation = _equation.Replace(_subEquation, temp.ToString());


                }
                else
                {
                    _replacedEquation = _equation;
                }

                // Calculate Sqrt
                if (_equation.Contains("\\sqrt"))
                {
                    string _sqrtReplaced = _replacedEquation.Replace(_replacedEquation, @"\sqrt");


                    int posA = _replacedEquation.IndexOf(@"\sqrt");
                    if (posA != -1)
                    {
                       _finalEquation  = _replacedEquation.Substring(0, posA);
                    }

                }
                else
                {
                    _finalEquation = _replacedEquation;
                }

                if (freqOpenBracket != freqCloseBracket)
                {
                    _finalEquation = _finalEquation + ")";
                };


            }
            catch (Exception ex)
            {
                log.Error(ex.InnerException);
            }

            rollingCount = CalculateInternalToBracket(_finalEquation, x);

            #endregion

            log.Info("Equation result : " + rollingCount);
            return rollingCount;
        }

        public static double CalculateInternalToBracket(string _internalEquation, string x_value)
        {
             double result2;

            _internalEquation = _internalEquation.Replace(" ", "");
            _internalEquation = _internalEquation.Replace("r", "");
            _internalEquation = _internalEquation.Replace("n", "");
            _internalEquation = _internalEquation.Replace("t", "");

            _internalEquation = _internalEquation.Replace("x", x_value);
            _internalEquation = _internalEquation.Replace("X", x_value);
            try
            {
                result2 = Convert.ToDouble(new DataTable().Compute(_internalEquation, null));  // basic internal bracket calculation using dataTable parsing 
            }
            catch(Exception ex)
            { 
                result2 = '0';
                log.Error(ex.InnerException);
            }
            return result2;

        }

        public static string EquationBetweenIndexes(string _equation, int bracketIndex)
        {

            string truncString = "";

            if (bracketIndex >= 0)
            {
                var startTrunc = IndexOfNth(_equation, '(', bracketIndex);
                var endTrunc = IndexOfNth(_equation, ')', bracketIndex);

                truncString = _equation.Substring(startTrunc+1, endTrunc-1).ToString();
            }
            else
            {
                truncString = _equation; 
            }
            return truncString;

        }

        private static int IndexOfNth(string str, char c, int bracketIndex)
        {
            var ordinal = str.IndexOf(c, bracketIndex);
            return str.IndexOf(c, bracketIndex); // get the index of first quote

        }
    }
}
