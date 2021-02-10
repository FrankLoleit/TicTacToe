using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TicTacToe
{
    class AiPlayer : Player
    {
        string chosenField;
        byte optionsToChoose;
        

        public AiPlayer()
        {
            sign = "O";
        }

        public string ExecuteAiTurn(List<Field> fields)
        {
            

            optionsToChoose = 0;
            if(!CompleteOrPreventRow(fields, GameLogic.rows, "completeRow"))
            {
                if (!CompleteOrPreventRow(fields, GameLogic.rows, "preventRow"))
                {
                    if (!FindDoubleOption(fields, GameLogic.rows, "completeRow"))
                    {
                        if (!FindDoubleOption(fields, GameLogic.rows, "preventRow"))
                            SearchRandomField(fields);
                    } 
                }
            }
            sign = "O";
            optionsToChoose = 0;
            return chosenField;
        }

        List<Field> CopyList(List<Field> fields)
        {
            List<Field> fieldsCopy = new List<Field>();
            for (int i = 0; i < fields.Count; i++)
            {
                fieldsCopy.Add(new Field(("Button" + (i + 1).ToString()), fields[i].occupied, fields[i].occupiedBy));
            }
            return fieldsCopy;
        }

        bool FindDoubleOption(List<Field> fields, int[,] rows, string completeOrPrevent)
        {
            if (completeOrPrevent == "completeRow")
                sign = "O";
            else
                sign = "X";
            
            List<Field> testList = CopyList(fields);
            for (int i = 0; i < testList.Count; i++)
            {
                if (!testList[i].occupied)
                {
                    testList[i].occupied = true;
                    testList[i].occupiedBy = sign;

                    if (CompleteOrPreventRow(testList, rows, completeOrPrevent) && optionsToChoose == 2)
                    {
                        chosenField = testList[i].btnName;
                        Trace.WriteLine("Found Double "+completeOrPrevent+" at "+chosenField+"\n");
                        testList[i].occupied = false;
                        testList[i].occupiedBy = "";
                        return true;
                    }
                    testList[i].occupied = false;
                    testList[i].occupiedBy = "";
                }
            }
            return false;
        }

        bool CompleteOrPreventRow(List<Field> fields, int[,] rows, string completeOrPrevent)
        {
            optionsToChoose = 0;
            for (int i = 0; i < rows.GetLength(0); i++)
            {
                int amountX_inRow = 0;
                int amountO_inRow = 0;
                string fieldCandidate = "";

                for (int j = 0; j < rows.GetLength(1); j++)
                {
                    string btnContent = fields[rows[i, j]].occupiedBy;
                    switch (btnContent)
                    {
                        case "X":
                            amountX_inRow++;
                            break;
                        case "O":
                            amountO_inRow++;
                            break;
                        default: //No btnContent means field is empty and could be picked
                            fieldCandidate = fields[rows[i, j]].btnName;
                            break;
                    }
                }

                if (completeOrPrevent == "completeRow")
                {
                    if (amountO_inRow == 2 && amountX_inRow == 0)
                    {
                        chosenField = fieldCandidate;
                        optionsToChoose++;
                    }
                }
                else
                {
                    if (amountX_inRow == 2 && amountO_inRow == 0)
                    {
                        chosenField = fieldCandidate;
                        optionsToChoose++;
                    }
                }
                
            }
            if (optionsToChoose > 0) 
                {
                    Trace.WriteLine(completeOrPrevent  + chosenField + "\nOptions to choose: " + optionsToChoose.ToString());
                    return true; 
                }
            else
                return false;
        }

        void SearchRandomField(List<Field> fields)
        {
            List<Field> freeFields = new List<Field>();
            for (int i = 0; i < fields.Count; i++)
            {
                if (!fields[i].occupied)
                    freeFields.Add(new Field(fields[i].btnName, false, ""));
            }

            //Choosing a random field from free fields and returning it
            Random rnd = new Random();
            int index = rnd.Next(freeFields.Count);

            chosenField = freeFields[index].btnName;
        }
    }
}
