namespace Equation_Calculator_Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please provide your equation to be calculated.");
            Console.WriteLine("Press enter to start the calculation.");
            string inputEquation = Console.ReadLine().Trim();
            //string inputEquation = " 10 - ( 2 + 3 * ( 7 - 5 ) )".Trim();
            if (!string.IsNullOrEmpty(inputEquation))
            {
                decimal? finalResult = Calculate(inputEquation);

                if (finalResult == null)
                {
                    Console.WriteLine("An error has occur.");
                }
                else
                {
                    Console.WriteLine("The calculation result of the equation is " + finalResult.ToString());
                }
            }
            else
            {
                Console.WriteLine("No equation is inputed.");
            }
            Console.WriteLine("Press enter to exit the application.");
            Console.ReadLine();
        }

        public static decimal? Calculate(string strInput) {
            string[] inputEquationElementArray = strInput.Split(" ");
            List<ArrayItem> lstArrayItem = new List<ArrayItem>();
            for (int i = 0; i < inputEquationElementArray.Length; i++)
            {
                lstArrayItem.Add(new ArrayItem(i + 1, inputEquationElementArray[i]));
            }

            lstArrayItem.Insert(0, new ArrayItem(0, "("));
            lstArrayItem.Add(new ArrayItem(lstArrayItem.Count, ")"));

            if (inputEquationElementArray.Length % 2 == 1 && inputEquationElementArray.Length> 1)
            {
                return RunCalculation(lstArrayItem);
            }
            else
            {
                Console.WriteLine("Invalid equation.");
            }
            return null;
        }

        public static decimal? RunCalculation(List<ArrayItem> lstArrayItem)
        {
            List<Equation> lstEquation = new List<Equation>();
            int count = 1;

            List<ArrayItem> lstOpenBracket = lstArrayItem.Where(x => x.content == "(").Reverse().ToList();
            List<ArrayItem> lstCloseBracket = lstArrayItem.Where(x => x.content == ")").ToList();

            foreach (ArrayItem openBrackets in lstOpenBracket) {
                Equation equation;
                if (lstCloseBracket.Find(x => x.index > openBrackets.index) != null)
                {
                    decimal equationResult = 0;
                    int closeBracketIndex = lstCloseBracket.Find(x => x.index > openBrackets.index).index;
                    lstCloseBracket = lstCloseBracket.Where(x => x.index != closeBracketIndex).ToList();
                    equation = new Equation("E" + count.ToString(), openBrackets.index, closeBracketIndex);

                    List<ArrayItem> itemRangeForThisEquation = lstArrayItem.Where(x => x.index > openBrackets.index && x.index < closeBracketIndex).ToList();
                    List<ArrayItem> bracketWithinThisEquation = itemRangeForThisEquation.Where(x => x.content == "(").ToList();
                    if (bracketWithinThisEquation.Count > 0)
                    {
                        foreach (ArrayItem bracketItem in bracketWithinThisEquation) 
                        {
                            int index = itemRangeForThisEquation.IndexOf(bracketItem);
                            if (index >= 0)
                            {
                                Equation tempEquation = lstEquation.Find(x => x.startIndex == bracketItem.index);
                                itemRangeForThisEquation.RemoveRange(index, (tempEquation.endIndex - tempEquation.startIndex + 1));
                                itemRangeForThisEquation.Insert(index, new ArrayItem(0, tempEquation.finalResult.ToString()));
                            }
                        }
                    }

                    List<ArrayItem> lstPowerOperands = itemRangeForThisEquation.Where(x => x.content == "^").ToList();
                    List<ArrayItem> lstMultipleDivisionOperands = itemRangeForThisEquation.Where(x => x.content == "*" || x.content == "/" || x.content == "%").ToList();
                    List<ArrayItem> lstAdditionSubstractionOperands = itemRangeForThisEquation.Where(x => x.content == "+" || x.content == "-").ToList();

                    if (lstPowerOperands.Count > 0) {
                        foreach (ArrayItem powerOperand in lstPowerOperands) 
                        {
                            int Index = itemRangeForThisEquation.IndexOf(powerOperand);
                            equationResult = int.Parse(itemRangeForThisEquation[Index - 1].content) ^ int.Parse(itemRangeForThisEquation[Index + 1].content);
                            itemRangeForThisEquation.RemoveRange(Index - 1, 3);
                            itemRangeForThisEquation.Insert(Index - 1, new ArrayItem(0, equationResult.ToString()));
                        }
                    }

                    if (lstMultipleDivisionOperands.Count > 0)
                    {
                        foreach (ArrayItem multipleDivisionOperand in lstMultipleDivisionOperands)
                        {
                            int Index = itemRangeForThisEquation.IndexOf(multipleDivisionOperand);
                            switch (multipleDivisionOperand.content) 
                            {
                                case "*":
                                    equationResult = decimal.Parse(itemRangeForThisEquation[Index - 1].content) * decimal.Parse(itemRangeForThisEquation[Index + 1].content);
                                    break;
                                case "/":
                                    equationResult = decimal.Parse(itemRangeForThisEquation[Index - 1].content) / decimal.Parse(itemRangeForThisEquation[Index + 1].content);
                                    break;
                                case "%":
                                    equationResult = decimal.Parse(itemRangeForThisEquation[Index - 1].content) % decimal.Parse(itemRangeForThisEquation[Index + 1].content);
                                    break;
                            }
                            itemRangeForThisEquation.RemoveRange(Index - 1, 3);
                            itemRangeForThisEquation.Insert(Index - 1, new ArrayItem(0, equationResult.ToString()));
                        }
                    }

                    if (lstAdditionSubstractionOperands.Count > 0)
                    {
                        foreach (ArrayItem additionSubstractionOperands in lstAdditionSubstractionOperands)
                        {
                            int Index = itemRangeForThisEquation.IndexOf(additionSubstractionOperands);
                            switch (additionSubstractionOperands.content)
                            {
                                case "+":
                                    equationResult = decimal.Parse(itemRangeForThisEquation[Index - 1].content) + decimal.Parse(itemRangeForThisEquation[Index + 1].content);
                                    break;
                                case "-":
                                    equationResult = decimal.Parse(itemRangeForThisEquation[Index - 1].content) - decimal.Parse(itemRangeForThisEquation[Index + 1].content);
                                    break;
                            }
                            itemRangeForThisEquation.RemoveRange(Index - 1, 3);
                            itemRangeForThisEquation.Insert(Index - 1, new ArrayItem(0, equationResult.ToString()));
                        }
                    }
                    equation.finalResult = equationResult;
                    lstEquation.Add(equation);
                }
                else {
                    return null;
                }
            }

            return lstEquation.Last().finalResult;
        }

    }
}